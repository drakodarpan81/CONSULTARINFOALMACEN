-- DROP FUNCTION cmb_mostrar_categoria_articulos();
-- DROP TYPE type_articulos_almacen;

CREATE OR REPLACE FUNCTION cmb_mostrar_categoria_articulos(opcion SMALLINT)
RETURNS SETOF type_articulos_almacen AS
$$

	DECLARE
		reg RECORD;

	BEGIN
	
		IF opcion = 0 THEN
			FOR reg IN  SELECT CONCAT( b.numero_categoria, '-', LPAD(a.folio_articulo::TEXT,4,'0'), ' | ', a.nombre_articulo )::VARCHAR(200) AS descripcion, a.id AS Identificador
						FROM tb_articulosalmacen AS a
						JOIN tb_categorias AS b ON (a.numero_categoria_id = b.id)
						WHERE a.status = 0 
						ORDER BY b.numero_categoria, a.folio_articulo
			LOOP
				RETURN NEXT reg;
			END LOOP;
			RETURN;
		ELSIF opcion = 1 THEN
			FOR reg IN  SELECT CONCAT( b.numero_categoria, '-', LPAD(a.folio_articulo::TEXT,4,'0'), ' | ', a.nombre_articulo )::VARCHAR(200) AS descripcion, a.id AS Identificador
						FROM tb_articulosalmacen AS a
						JOIN tb_categorias AS b ON (a.numero_categoria_id = b.id)
						WHERE a.status = 1
						ORDER BY b.numero_categoria, a.folio_articulo
			LOOP
				RETURN NEXT reg;
			END LOOP;
			RETURN;
		ELSIF opcion = 2 THEN
			FOR reg IN SELECT CONCAT( numero_categoria, ' - ', nombre_categoria  )::VARCHAR(200) AS descripcion, id AS Identificador
					   FROM tb_categorias 
					   WHERE status = 0
					   ORDER BY numero_categoria
			LOOP
				RETURN NEXT reg;
			END LOOP;
			RETURN;
		ELSIF opcion = 3 THEN
			FOR reg IN SELECT CONCAT( numero_categoria, ' - ', nombre_categoria  )::VARCHAR(200) AS descripcion, id AS Identificador
					   FROM tb_categorias 
					   WHERE status = 1
					   ORDER BY numero_categoria
			LOOP
				RETURN NEXT reg;
			END LOOP;
			RETURN;
		END IF;
	END

$$
LANGUAGE 'plpgsql';
