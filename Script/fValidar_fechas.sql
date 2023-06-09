-- DROP FUNCTION fValidar_fechas();

CREATE OR REPLACE FUNCTION fValidar_fechas(dFechaInicio DATE, dFechaFin DATE, OUT nDias INTEGER)
RETURNS INTEGER AS
$$

	DECLARE
		dias_dif INTEGER;

	BEGIN

		dias_dif := (SELECT DATE_PART('Day', dFechaInicio::TIMESTAMP - dFechaFin::TIMESTAMP) AS diferencia_dias);
		
		nDias = dias_dif;
		
	END

$$
LANGUAGE 'plpgsql';