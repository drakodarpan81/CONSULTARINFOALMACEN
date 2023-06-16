DROP FUNCTION fConsultar_informacion_almacen(opcion SMALLINT, 
														  nCategoria INTEGER, 
														  nFolio INTEGER,
														  nConsultaFecha SMALLINT,
														  dFechaInicio DATE, 
														  dFechaFin DATE, 
														  nStatus SMALLINT,
														  sLote VARCHAR(50));
DROP TYPE type_consultar_historial;

CREATE TYPE type_consultar_historial AS
(
	folio_articulo		INTEGER,
	numero_categoria	INTEGER, 
	nombre_categoria	VARCHAR(50),
	nombre_articulo 	VARCHAR(50),
	cantidad 			INTEGER,
	stock 				INTEGER,
	des_presentacion 	VARCHAR(50),
	nombre_proveedor 	VARCHAR(50),
	requisicion 		VARCHAR(20),
	fecha_caducidad		DATE,
	lote_caducidad		VARCHAR(20),
	marca_caducidad		VARCHAR(50),
	observacion 		VARCHAR(100),
	des_nombreusuario	VARCHAR(150),
	folio_movimiento	INTEGER,
	fecha_modificacion	DATE
);

CREATE OR REPLACE FUNCTION fConsultar_informacion_almacen(opcion SMALLINT, 
														  nCategoria INTEGER, 
														  nFolio INTEGER,
														  nConsultaFecha SMALLINT,
														  dFechaInicio DATE, 
														  dFechaFin DATE, 
														  nStatus SMALLINT,
														  sLote VARCHAR(50))
RETURNS SETOF type_consultar_historial AS
$$

	DECLARE
		reg RECORD;
		nId INTEGER;
		cSql VARCHAR;
		cStatus VARCHAR;
		cFechas VARCHAR;

	BEGIN

		cSql = '';
		cStatus = '';
		cFechas = '';

		SELECT id
		INTO nId
		FROM tb_categorias
		WHERE numero_categoria = nCategoria;
		
		IF opcion = 0 THEN
			cSql = 'WHERE a.numero_categoria_id = '||nId||' AND a.folio_articulo = '||nFolio;
		ELSIF opcion = 1 THEN
			cSql = 'WHERE a.numero_categoria_id = '||nId;			
		ELSIF opcion = 2 THEN
			cSql = 'WHERE a.lote_caducidad = '''||sLote||''' ';
		END IF;
		
		IF nStatus = 0 THEN
			cStatus = ' AND a.status = 0';
		ELSIF nStatus = 1 THEN
			cStatus = ' AND a.status = 1';
		END IF;
		
		IF nConsultaFecha = 1 THEN
			cFechas = ' AND a.fecha_modificacion BETWEEN '''||dFechaInicio||''' AND '''||dFechaFin||''' ';
		END IF;
		
		FOR reg IN EXECUTE FORMAT('SELECT a.folio_articulo, b.numero_categoria, b.nombre_categoria, a.nombre_articulo, a.cantidad, a.stock, c.des_presentacion, d.nombre_proveedor, 
										  a.requisicion, a.fecha_caducidad, a.lote_caducidad, a.marca_caducidad, a.observacion, T2.des_nombreusuario, a.folio_movimiento, 
										  a.fecha_modificacion
								   FROM tb_his_articulos_almacen AS a
								   JOIN tb_categorias AS b ON (a.numero_categoria_id = b.id)
								   JOIN tb_presentacion AS c ON (a.presentacion_id = c.id)
								   JOIN tb_proveedor AS d ON (a.proveedor_id = d.id),
								   dblink(''dbname=personalLESP user=personal password=be422fac5ea0da9dbd3434162cd8ccff'', ''SELECT id_identificador, des_nombreusuario FROM cat_personal'') AS T2 (id_identificador INTEGER, des_nombreusuario VARCHAR(150))
								   '|| cSql ||'
								   '|| cStatus ||'
								   '|| cFechas ||'
								   AND a.id_empleado = T2.id_identificador
								   ORDER BY a.numero_categoria_id, a.folio_articulo')
		LOOP
			RETURN NEXT reg;
		END LOOP;
		RETURN;
	
	END

$$
LANGUAGE 'plpgsql';