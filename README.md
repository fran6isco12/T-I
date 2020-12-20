# T-I
trabajo integral GLF-s2.
desarrollado en visual studio 2019.
trabajo integral grafos(hoja de ruta)
requerimientos: net framework 4.5 o superior.
instrucciones de uso:
1.copiar las carpetas servicios-ti y cliente-ti que se encuentran en la carpeta dist a la carpeta wwwroot.
2.copiar la carpeta grafos-ti a la raiz del dico local principal.
3.dentro de la carpeta grafos-ti crear una carpeta tmp esta contendra los logs.
4.en el administrador de iss cear un nuevo sitio nombrarla sitios-ti, bajo la configuracion de net 4.5,seleccionar la ruta dentro de wwwroot donde se encuentra sericio-ti, y 
el puerto asignado debe ser el 5151.
5.crear un nuevo sitio llamado cliente-ti , bajo la configuracion de net 4.5,seleccionar la ruta dentro de wwwroot donde se encuentra cliente-ti, y 
el puerto asignado debe ser el 3030.
6.la ruta de acceso del cliente sera http://localhost:3030/index.aspx.
7.antes de correr el cliente modificar el archivo parametros manteniendo el formato T;N;x,y(T:puede ser C o P dependiendo si es un centro de distribucion o un punto de venta;
N:indentificador numerico;x,y:coordenadas del lugar deben ser numeros enteros;)
8.correr el cliente.
9.al presionar el boton leer parametros se cargaran los datos.
10.una vez cargados los parametros ingresar los despachhos.
11.tras haber ingresados los parametros al presionar hoja de ruta esta se generara dentro de la carpeta grafos-ti un mensaje emergente indicara bajo que nombre quedo guardado.
