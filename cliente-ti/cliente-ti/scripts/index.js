window.onload = function () {

    var id = 0;
    var centros = new Array();
    var puntos = new Array();
    var carga = 0;
    var puntoventa;
    var centrodd;
    var respuesta = "a";
    function comparar(a, b) { return a - b }

    function leerparametros() {
        axios.get('http://localhost:5151/api/values/' + id)
            .then(function (response) {
                respuesta = response.data;
                console.log("ejecutado:" + id);
                console.log(respuesta);
                alert(response.data);
            })
            .catch(function (error) {
                console.log(error);
                alert("Error");
            })
            .then(function () {
            });
    }

    function getcentros() {
        axios.get('http://localhost:5151/api/values/centros/' + id)
            .then(function (response) {
                centros = response.data;
                console.log("centros obtenidos:" + response.data);
                console.log(centros.length)
            })
            .catch(function (error) {
                console.log(error);
                alert("Error al obtener centros de distribucion");
            })
            .then(function () {
            });
    }
    function getpuntos() {
        axios.get('http://localhost:5151/api/values/puntos/' + id)
            .then(function (response) {
                puntos = response.data;
                console.log("ejecutado:" + id);
                console.log("puntos obtenidos:" + response.data);
                console.log(puntos[1]);
            })
            .catch(function (error) {
                console.log(error);
                alert("Error al obtener puntos de venta");
            })
            .then(function () {
            });
    }
    function gethojaruta() {
        axios.get('http://localhost:5151/api/values/hojaruta/' + id)
            .then(function (response) {
                puntos = response.data;
                console.log(response.data);
                alert(response.data);
            })
            .catch(function (error) {
                console.log(error);
                alert("Error al obtener ruta");
            })
            .then(function () {
            });
    }

    function agregardespacho() {
        axios({
            method: "post",
            url: "http://localhost:5151/api/values/agregar/"+id,
            headers: {
                "Content-Type": "application/json"
            },
            data: {
                "pv": puntoventa,
                "pvp": carga,
                "cdd": centrodd
            }

        })
            .then(function (response) {
                console.log(response.data);
                console.log("ejecutado:carga=" + carga + " punto=" + puntoventa + " centro=" + centrodd)
                var para = document.createElement("p");
                var node = document.createTextNode("carga=" + carga + " punto=" + puntoventa + " centro=" + centrodd);
                para.appendChild(node);
                var element = document.getElementById("panel");
                element.appendChild(para);
            })
            .catch(function (error) {
                console.log("error");
                alert("Error");

            })
            .then(function () {
            });
    }

    function llenar() {
        centros = centros.sort(function comparar1(a, b) { return parseInt(a) - parseInt(b) });
        puntos = puntos.sort(function comparar2(a, b) { return parseInt(a) - parseInt(b) });
        var select = document.getElementById('sel1');
        for (var i = 0; i < centros.length; i++) {
            var opt = document.createElement('option');
            opt.value = centros[i];
            opt.innerText = "centro "+centros[i];
            select.appendChild(opt);
        }
        select = document.getElementById('sel2');
        for (var j = 0; j <= puntos.length-1; j++) {
            var opt = document.createElement('option');
            opt.value = puntos[j];
            opt.innerText = "punto " + puntos[j];
            select.appendChild(opt);
        }

    }

    function iniciar() {
        respuesta="a"
        id = id + 1;
        puntos = new Array();
        centros = new Array();
        leerparametros();
        setTimeout(() => { getcentros(); }, 1000);
        setTimeout(() => { getpuntos(); }, 2000);
        setTimeout(() => {
            if (respuesta == "parametros agregados") { llenar() }
            else {
                alert(respuesta)
            }
        },3000);

    }
    function addrun() {
        carga = document.getElementById("carga").value;
        puntoventa = document.getElementById("sel2").value;
        centrodd = document.getElementById("sel1").value;
        if (carga.includes(",") == false) {
            if (carga.includes("-") == false) {
                if (carga.includes("+") == false) {
                    if (carga.includes(".") == false) {
                        if (carga.includes("e") == false) {
                            carga = parseInt(carga)
                            if (carga >= 1) {
                                if (carga <= 1000) {
                                    puntoventa = parseInt(puntoventa);
                                    centrodd = parseInt(centrodd);
                                    agregardespacho();
                                }
                                else {
                                    alert("la carga debe ser menor o igual a mil");
                                }
                            }
                            else {
                                alert("la carga debe ser mayor o igual a 1");
                            }
                        }
                        else {
                            alert("la carga debe ser un numero entero positivo");
                        }
                    }
                    else {
                        alert("la carga debe ser un numero entero positivo");
                    }
                }
                else {
                    alert("la carga debe ser un numero entero positivo");
                }
            }
            else {
                alert("la carga debe ser un numero entero positivo");
            }
        }
        else {
            alert("la carga debe ser un numero entero positivo");
        }
    }
    function comprobar() {
        if (id == 0) {
            iniciar()
        }
        else {
            alert("parametros ya cargados presione F5 para cargar un nuevo archivo")
        }
    }
    document.getElementById("parametros").onclick = comprobar;
    document.getElementById("Despacho").onclick = addrun;
    document.getElementById("hojar").onclick = gethojaruta;
    }
