<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TPC_GROSS_LAINO_CHAPARRO.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        body {            
            background-image: url("../img/fondo-principal.jpg");
            background-color: #FFFFFF4D !important;
            width: 100%;
            height: 100vh;
            background-size: cover;
            background-position: center;
        }        
    </style>

    <h1 class="nom-stl">Lubricentro Tony</h1>

    <center>
    <div id="carouselExampleCaptions" class="carousel slide crs-index" data-ride="carousel">
        <ol class="carousel-indicators">
            <li data-target="#carouselExampleCaptions" data-slide-to="0" class="active"></li>
            <li data-target="#carouselExampleCaptions" data-slide-to="1"></li>
            <li data-target="#carouselExampleCaptions" data-slide-to="2"></li>
        </ol>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="img/carrusel/aceites-variados.jpg" class="d-block w-100 img-car-1" alt="...">
                <div class="carousel-caption d-none d-md-block txt-desc-car">
                    <h5 class="ttl-car">Multimarca</h5>
                    <p>Utilizamos variedad de los mejores aceites del mercado.</p>
                </div>
            </div>
            <div class="carousel-item">
                <a href="turnos.aspx"><img src="img/carrusel/turnos.jpg" class="d-block w-100 img-car-1" alt="..."></a>
                <div class="carousel-caption d-none d-md-block txt-desc-car">
                    <h5 class="ttl-car">Turnos Online</h5>
                    <p>Reserva tu turno online haciendo click en la imagen.</p>
                </div>
            </div>
            <div class="carousel-item">
                <a href="contacto.aspx"><img src="img/carrusel/contactanos.jpg" class="d-block w-100 img-car-1" alt="..."></a>
                <div class="carousel-caption d-none d-md-block txt-desc-car">
                    <h5 class="ttl-car">¡Contactanos!</h5>
                    <p>Ante cualquier duda contactanos haciendo click en la imagen.</p>
                </div>
            </div>
        </div>
        <a class="carousel-control-prev" href="#carouselExampleCaptions" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#carouselExampleCaptions" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>
    </center>

</asp:Content>
