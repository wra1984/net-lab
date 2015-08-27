CREATE PROCEDURE P_CONSULTA_CFOP
AS 
BEGIN
  SELECT Cfop, SUM(ValorIcms) AS ValorIcms, 
         SUM(BaseIcms) AS BaseIcms, 
         SUM(ValorIPI) AS ValorIPI, 
         SUM(BaseIPI) AS BaseIPI
    FROM NotaFiscalItem	
   GROUP BY Cfop 
END
