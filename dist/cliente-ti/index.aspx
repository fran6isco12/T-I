<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="cliente_ti.WebForm1" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="utf-8" />
    <title>GLF-TI</title>
</head>
<body runat="server">
    <div class="topnav">
        <a class="active" href="index.aspx">INICIO</a>
    </div>
    <div class="container">
        <div class="divL">
            <div class="container" id="container1">
                <div class="row">
                    <div >
                        <div class="col-xs-8">
                                <header class="heading" >Despachos</header>
                            </div>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-xs-4">
                                <label class="despacho">Centro de dis. :</label>
                            </div>
                            <div class="col-xs-8">

                             <select class="form-control" id="sel1">
                             </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-xs-4">
                                <label class="despacho">Punto de venta :</label>
                            </div>
                            <div class="col-xs-8">

                             <select class="form-control" id="sel2">
                             </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-xs-4">
                                <label class="despacho">Entrega:</label>
                            </div>
                            <div class="col-xs-8">
                                <input min="1" type="number" step="1" id="carga" class="form-control">
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-xs-8">
                                <input class="btn btn-primary" type="button" id="Despacho" value="Ingresar despacho">
                            </div>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
            </div>
        <div class="divR">
            <div class="container" id="container2">
                <div class="row">
                    <div>                        
                        <div class="col-xs-8">
                                <header class="heading" >Opciones</header>
                            </div>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-xs-8">
                                <input class="btn btn-primary" type="button" id="parametros" value="Leer Parametros">
                                
                            </div>
                            <div class="col-xs-4">
                                <input class="btn btn-primary" type="button" id="hojar" value="Hoja de ruta">
                                </div>
                        </div>
                    </div>
                        <div class="col-sm-12">
                            <div class="row">
                            <div class="panel panel-default">
                                <div class="panel-body" id="panel" style='width:100%;height:150px;overflow-y:scroll'>
                                    <p style="color:black;text-align:left;">despachos:</p>     
                                </div>
                            </div>
                                </div>
                        </div>

                    </div>
                   </div>
                </div>
        </div>
    </div>
 
        <div><footer>GLFTI-2020S2</footer></div>
</body>

</html>
<script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<link href="//netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
<script src="//netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>
<link href="public/HF.css" rel="stylesheet" />
<link href="public/others.css" rel="stylesheet" />
<script src="https://unpkg.com/axios/dist/axios.min.js"></script>
<script src="scripts/index.js"></script>
