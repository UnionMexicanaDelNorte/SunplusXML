var http = require('http');
var url  = require('url');
var sql = require('mssql'); 

var config = {
    user: 'sa',
    password: 'SunPlus7!',
    server: 'localhost', 
    database: 'SUNPLUSADV',
    options: {
        encrypt: true // Use this if you're on Windows Azure
    }
}
http.createServer(function (req, res) {
  	res.writeHead(200, {'Content-Type': 'text/plain'});
 	var url_parts = url.parse(req.url, true);
    var query = url_parts.query;    
	switch(parseInt(query.accion))
	{
		case 1:
var date = new Date();
var current_hour = date.getHours();

var current_year= date.getFullYear();
var current_month= parseInt( date.getMonth())+1;
if(current_month<10)
{
  current_month="0"+current_month;
}
var current_day= parseInt(date.getDate());
if(current_day<10)
{
  current_day="0"+current_day;
}
var initSalt = '%&/mysalt5=';
var endSalt = 'ThisIsMyEasyToRememberSalt';

var clave = initSalt+current_year+current_month+current_day+endSalt;


var crypto = require('crypto'),
        text = clave,
        key = '7HolaAmigosComoEstan7';

    // create hahs
    var hash = crypto.createHmac('sha512', key);
    hash.update(text);
    var ei = hash.digest('hex');

      if(ei===query.ei)
      {
        sql.connect(config, function(err) {
          console.log(err);
          var request = new sql.Request();
          var querySQL = "SELECT ASSET_CODE,DESCR,START_PERD,LAST_PERD, BASE_GROSS,BASE_DEP,BASE_NET, BASE_PCENT FROM [SUNPLUSADV].[dbo].[CEA_ASSET] WHERE ASSET_CODE =  '"+query.argumento1+"'";
          request.query(querySQL, function(err, recordset) {
             console.log(err);
              res.end(JSON.stringify(recordset));
          });
        });
      }
      else
      {
         res.end("No dijiste la palabra correcta :P");
      }
			
		break;
    case 2:
      sql.connect(config, function(err) {
          var request = new sql.Request();
          var querySQL = "SELECT STATUS FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE folioFiscal =  '"+query.argumento1+"'";
          request.query(querySQL, function(err, recordset) {
          var bandera=0;
            if(recordset.length==0)
            {//factura no esta en la base de datos
              bandera=1;
              res.end("{\"success\" : -1}");
            }
          
if(bandera==0)
{
if(recordset[0].STATUS==='1')
             {
                var request0 = new sql.Request();
                var querySQL0 = "SELECT total FROM [SU_FISCAL].[dbo].[facturacion_XML] WHERE folioFiscal =  '"+query.argumento1+"'";
                var cantidad =0;
                request0.query(querySQL0, function(err, recordset0) {
             
                  cantidad = parseFloat(recordset0[0].total);
               
                  var request1 = new sql.Request();
                  var querySQL1 = "SELECT SUM(AMOUNT) as amount FROM [SU_FISCAL].[dbo].[FISCAL_xml] WHERE FOLIO_FISCAL = '"+query.argumento1+"' GROUP BY FOLIO_FISCAL";
                  request1.query(querySQL1, function(err, recordset1) {
                
                    if(recordset1.length>0)
                    {
                      cantidad = cantidad-parseFloat(recordset1[0].amount);
                    }
                    var requestx = new sql.Request();
                    var querySQLx = "SELECT SUM(amount) as amount FROM [SU_FISCAL].[dbo].[_prepolizas] WHERE UUID = '"+query.argumento1+"' GROUP BY UUID";
                    requestx.query(querySQLx, function(err, recordsetx) {
                       if(recordsetx.length>0)
                    {
                      cantidad = cantidad-parseFloat(recordsetx[0].amount);
                    }

                    if(cantidad==0)
                      {
                         res.end("{\"success\" : -2}");
                        }

var request2 = new sql.Request();
                    //gastos
                    var querySQL2 = "SELECT c.nombre, c.idConcepto  FROM [SU_FISCAL].[dbo].[_permisos] p INNER JOIN [SU_FISCAL].[dbo].[_conceptos] c on c.idConcepto = p.idConcepto WHERE p.WHO = '"+query.argumento2+"' AND c.tipo = 1 order by nombre asc";
                    request2.query(querySQL2, function(err, recordset2) {
                      var request3 = new sql.Request();
                      //recursos
                      var querySQL3 = "SELECT c.nombre, c.idConcepto  FROM [SU_FISCAL].[dbo].[_permisos] p INNER JOIN [SU_FISCAL].[dbo].[_conceptos] c on c.idConcepto = p.idConcepto WHERE p.WHO = '"+query.argumento2+"' AND c.tipo = 2 order by nombre asc";
                      request3.query(querySQL3, function(err, recordset3) {
 res.end("{\"success\" : 1, \"cantidad\" : "+cantidad+" , \"gastos\" : "+JSON.stringify(recordset2)+"  , \"recursos\" : "+JSON.stringify(recordset3)+"}");
                      });
                    });

                 });
                  });
                });
             }
             else
             {//factura cancelada
                res.end("{\"success\" : 0}");
             }
}
 else
             {//factura cancelada
                res.end("{\"success\" : 0}");
             }
             
             
          });
        });

    break;
     case 3:
     sql.connect(config, function(err) {
            var request = new sql.Request();
            var querySQL = "INSERT INTO [SU_FISCAL].[dbo].[_prepolizas] (WHO,UUID, amount,idConceptoGasto,contabilizado,idConceptoRecurso) values ('"+query.argumento1+"','"+query.argumento2+"',"+query.argumento3+","+query.argumento4+",0,"+query.argumento5+")";
            request.query(querySQL, function(err, recordset) {
                res.end("{\"success\" : 1}");
            });
        });

    break;
    case 4:
     sql.connect(config, function(err) {
            var request = new sql.Request();
            var querySQL = "INSERT INTO [SU_FISCAL].[dbo].[_tokens] (WHO,token, tipo) values ('"+query.argumento1+"','"+query.argumento2+"',"+query.argumento3+")";
            request.query(querySQL, function(err, recordset) {
                res.end("{\"success\" : 1}");
            });
        });
    break;
    case 5://presupuesto del mes
     sql.connect(config, function(err) {
      //get fnct
            var request1 = new sql.Request();
            var querySQL1 = "SELECT FNCT FROM  [SU_FISCAL].[dbo].[_FNCTyWHO] WHERE WHO =  '"+query.argumento1+"'";
            request1.query(querySQL1, function(err, recordset1) {
              var bandera=0;
              if(recordset1.length==0)
              {//factura no esta en la base de datos
                bandera=1;
                res.end("{\"success\" : -1}");
              }            
              if(bandera==0)
              {
                var request = new sql.Request();
                var querySQL = "SELECT DISTINCT b.ACCNT_CODE, SUM( b.AMOUNT) as amount, MAX(c.DESCR) as DESCR, 0 as gastado FROM [SUNPLUSADV].[dbo].[CEA_B_SALFLDG] b INNER JOIN [SUNPLUSADV].[dbo].[CEA_ACNT] c on c.ACNT_CODE = b.ACCNT_CODE WHERE b.ANAL_T3 = '"+recordset1[0].FNCT+"' AND b.ANAL_T6 = '"+query.argumento1+"' AND SUBSTRING( CAST(PERIOD AS NVARCHAR(10)),1,7) = '"+query.argumento2+"' GROUP BY b.ACCNT_CODE";
                request.query(querySQL, function(err, recordset) {
                    var request2 = new sql.Request();
                    var i =0;
                    for(i=0;i<recordset.length;i++)
                    {
                      recordset[i].amount = Math.abs( parseFloat(recordset[i].amount));
                      var querySQL2 = "SELECT ACCNT_CODE, AMOUNT, DESCRIPTN, TREFERENCE FROM [SUNPLUSADV].[dbo].[CEA_A_SALFLDG] WHERE ANAL_T3 = '"+recordset1[0].FNCT+"' AND ANAL_T6 = '"+query.argumento1+"' AND SUBSTRING( CAST(PERIOD AS NVARCHAR(10)),1,7) = '"+query.argumento2+"' AND ACCNT_CODE = '"+recordset[0].ACCNT_CODE+"'";
                      request2.query(querySQL2, function(err, recordset2) {
                        var gastado1 = 0;
                        var j=0;
                        for(j=0;j<recordset2.length;j++)
                        {
                          gastado1+=Math.abs( parseFloat(recordset2[j].AMOUNT));
                        }
                        recordset[i].gastado=gastado1;
                      });
                    }
                    res.end("{\"success\" : 1,  \"presupuesto\" : "+JSON.stringify(recordset)+"}");
                });
              }
            });            
        });
    break;
     case 6://BHP
     sql.connect(config, function(err) {
            var request = new sql.Request();
            var querySQL = "INSERT INTO [BHP].[dbo].[tokens] (token, tipo) values ('"+query.argumento1+"',"+query.argumento2+")";
            request.query(querySQL, function(err, recordset) {
                res.end("{\"success\" : 1}");
            });
        });
    break;
		default:
		break;
	}	
}).listen(90, '0.0.0.0');
