1.- En tu servidor sunplus, crea una nueva base de datos en sql server 2008 que se llame "SU_FISCAL"

2.- Aplicale el respaldo SU_FISCAL01.bak

3.- Copia la carpeta:  SunPlusXML/SunPlusXML/bin/Debug a la unidad C:

4.- Copia las Carpetas: "_FACTURAS" y "_PROGRAMAS_FISCAL" a la unidad S

5.- Ejecuta C:/Debug/SunPlusXML.exe , en el menu de BD, configura los datos de tu RFC y contraseña del buzón tributario, asi como los datos de tu conexión a la base de datos, también escribe el correo del contador del campo en el correo receptor, y en el correo emisor debe de ser un correo @gmail que tenga permitido la ejecución de aplicaciones inseguras, después presiona el botón "Salida" y selecciona la carpeta S:/_FACTURAS

6.- Cierra y vuelve a ejecutar el programa C:/Debug/SunPlusXML.exe para que ahora si agarre tu configuración personalizada desde el principio, espera a que termine y al final revisa la tabla SU_FISCAL.dbo.facturación_XML y la carpeta: S:/_FACTURAS para revisar que te haya descargado las facturas, para el correcto funcionamiento del programa, deben de existir facturas emitidas y recibidas del mes en curso, (si no existen no importa).

7.- Instala el programa timecomx_basic-132-x86.exe, abre 2 instancias del programa, en una configurala para que cada hora se ejecute el programa SunPlusXML.exe, y en la casilla de argumentos escribe el numero "1", para que entre en modo ligero (descarga solo las facturas del dia); en la otra instancia del programa, configurala para que se ejecute todos los dias de domingo a viernes, a las 12:50 a.m. sin argumentos, para que entre en modo pesado (todas las facturas del mes). Es importante que a las 2 instancias se les configure para que si se reinicie el servidor se vuelva a ejecutar el proceso automaticamente. Esto lo trate de hacer primeramente con las tareas programadas de windows, pero no me funciono..

8.- Edita el archivo S:/_PROGRAMAS_FISCAL/settings.txt y escribe el nombre de tu servidor

9.- Compila los formularios y asignaselos donde consideres conveniente, también puedes compilarlos y ver los argumentos de los botones "Ligar XML" y "Terminar de Ligar" para crear tus propios formularios, y asignarselos a tus tipos de diarios.

10.- En el menú inicial de sunplus del contador principal del campo, agrega un acceso directo al archivo S:/_PROGRAMAS_FISCAL/AdministradorXML.exe

11.- Ejecuta el programa AdministradorXML y en el menú "Administrar" debes de llenar "Mis datos", luego debes de llenar "Permisos cuentas" y así sucesivamente para que funcione bien el programa a la hora de ligar el diario con los CFDI.
