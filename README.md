1.- En tu servidor sunplus, aplica el respaldo SU_FISCAL01.bak a una base de datos nueva que se llame "SU_FISCAL"

3.- Copia la carpeta:  SunPlusXML/SunPlusXML/bin/Debug a la unidad C:

4.- Copia las Carpetas: "_FACTURAS" y "_PROGRAMAS_FISCAL" a la unidad S

5.- Haz un acceso directo a C:/Debug/SunPlusXML.exe , y en sus propiedades agregale el número 2, para que se ejecute en modo "ultrapesado" y descargue las facturas del todo el año, para que funcione tienes que ir al menu de BD, configura los datos de tu RFC y contraseña del buzón tributario, asi como los datos de tu conexión a la base de datos, también escribe el correo del contador del campo en el correo receptor, y en el correo emisor debe de ser un correo @gmail que tenga permitido la ejecución de aplicaciones inseguras, después presiona el botón "Salida" y selecciona la carpeta S:/_FACTURAS, tu correo de gmail debe de permitir aplicaciones menos seguras en: https://www.google.com/settings/security/lesssecureapps


6.- Una vez que termine el modo ultrapesado, instala el programa timecomx_basic-132-x86.exe, abre 2 instancias del programa, en una configurala para que cada hora se ejecute el programa SunPlusXML.exe, y en la casilla de argumentos escribe el numero "1", para que entre en modo ligero (descarga solo las facturas del dia); en la otra instancia del programa, configurala para que se ejecute todos los dias de domingo a viernes, a las 12:50 a.m. sin argumentos, para que entre en modo pesado (todas las facturas del mes). Es importante que a las 2 instancias se les configure para que si se reinicie el servidor se vuelva a ejecutar el proceso automaticamente. Esto lo trate de hacer primeramente con las tareas programadas de windows, pero no me funciono..

7.- Compila los formularios y asignaselos donde consideres conveniente, el formulario 0BLEX1 corresponde al formulario 1BLE01, el 0BLEX2 al 1BLE02 y asi, también puedes compilarlos y ver los argumentos de los botones "Ligar XML" y "Terminar de Ligar" para crear tus propios formularios, y asignarselos a tus tipos de diarios.

8.- En el menú inicial de sunplus del contador principal del campo, agrega un acceso directo al archivo S:/_PROGRAMAS_FISCAL/AdministradorXML.exe %JRNAL_SRCE% %BUNIT%  

donde %JRNAL_SRCE% es el usuario de sunplus y %BUNIT% es la unidad de negocio

9.- Ejecuta el programa AdministradorXML y en el menú "Administrar" debes de llenar "Mis datos", luego debes de llenar "Permisos cuentas" y así sucesivamente para que funcione bien el programa a la hora de ligar el diario con los CFDI.
