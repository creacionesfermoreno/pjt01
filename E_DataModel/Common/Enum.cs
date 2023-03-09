namespace E_DataModel.Common
{
    public enum Operation
    {
        Create = 0,
        Update = 1,
        Delete = 2,
        Validate = 3,
        Transaction = 4,
        UpdateEstado = 5,
        UpdateFoto = 6,
        UpdateClave = 7,
        UpdatePass = 8,



        UpdateMembresiaNroIngresos = 9,

        UpdateCorrelativo = 10,

        UpdateDescongelar = 11,

        UpdateCongelarProgramado = 12,

        UpdateFechaPago = 13,

        DeletePorSocio = 14,

        UpdateEstadoTraspaso = 17,

        UpdateMenbresiasAutomaticoInactivo = 18,

        UpdateConfiguracionAsistencia = 20,

        UpdateConfiguracionFreezing = 21,

        UpdateMembresiaFreezing = 22,

        deleteRutina = 24,

        UpdateAsistenciaInvitado = 26,

        CreateFiltro = 27,

        Eliminarfiltro = 28,

        RegistrarMigracion = 29,

        ActualizarEstadoEnvioCorreo = 30,

        CreateAgendaInvitado = 31,

        ActualizarMontoPago = 32,

        ActualizarEstadoAgendaSeguimiento = 33,

        CreateAgendaProspecto = 34,

        UpdateProspectoASocio = 36,

        UpdateProspectoAInvvitado = 37,


        RepartirClientes_Vendedores = 47,

        ActualizarEstado_RepartirClientes = 49,

        ActualizarDia_RepartirClientes = 50,

        ActualizarAsesorComercial_Cliente = 51,
        //
        //RealizarTraspaso = 52,

        Actualizar_Estado_Solicitud_Membresia_Denegado = 53,

        uspActualizarConfiguracion_Congelamiento = 54,

        uspActualizarConfiguracion_Traspaso_Duplicado = 55,

        uspRegistrarCompromiso = 56,

        uspActualizarCompromiso = 57,

        uspEliminarCompromiso = 58,

        Update_DatosAdicionales = 60,

        Create_DatosSalud = 63,

        Update_DatosSalud = 64,

        CreateCursoProfesor = 65,

        UpdateCursoProfesor = 66,

        CreateProfesorCurso = 67,

        UpdateProfesorCurso = 68,

        UpdateDisciplinaProfesor = 70,

        UpdateEstadoVentasOtros = 71,

        UpdateTipoSistema = 72,

        UpdateCorreoBienvenida = 73,

        Create_SociosDatosAdicionales = 74,

        Update_SociosDatosAdicionales = 75,

        Update_ImprimirContrato = 76,

        uspRegistrarAbrirCaja = 77,

        UpdateAbrirCaja = 78,

        DeleteAsistencia = 79,


        UpdateConfiguracionTiempoMarcarAsistencia = 80,

        Update_uspActualizarControlPagoSoftware = 81,

        Create_UspAgendaSeguimientoTodos = 82,

        Create_UspReagendarAgendaSeguimientoTodosCaidos = 83,

        UpdateInvitadoSocio = 84,

        UpdateReferidoASocio = 85,

        UpdateReferidoAInvitado = 86,

        UpdateLlamadaEAInvitado = 87,

        UpdateLlamadaEASocio = 88,

        uspActualizarAsistenciaInvitadoPorCodigoInvitado = 89,

        UpdateConfiguracionCitasCaidas = 90,

        UpdateConfiguracionCaja = 91,

        uspEnviarSocioANuevo = 92,

        CerrarCitaAgenda = 93,

        CreateConfiguracion_adFitness = 94,

        uspEliminarCliente_Configuracion_AdFitness = 95,

        UpdateConfiguracion_adFitness = 96,

        uspActualizarEstadoAdFitness_AtencionCliente = 97,

        SEGRegistrarPerfilMenuPermisos = 98,

        uspActualizarPagoSuplementosEstado = 99,

        uspActualizarDatosUsuarios = 100,

        uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo = 101,

        uspCambiarEstadoUsuarioConfiguracionNuevoMes = 102,

        uspRegistrarMetaInicioMes = 103,

        uspActualizarPagoRopasEstado = 104,

        RegistroMasivoHorarioClasesGrupales = 109,

        RegistroAsistenciaClasesGrupalesChecking = 110,

        RegistroAsistenciaInvitadosClasesGrupalesChecking = 111,

        RegistrarAsistenciaProfesionalFitness = 112,

        RegistrarAsistenciaPersonalAdministrativo = 113,

        CreateFiados = 114,

        CreatePagarFiados = 115,

        uspActualizarMenbresiasFechaInicio = 116,

        uspRegistrarPaqueteSedePermiso = 117,

        ActualizarFotoProfesor = 118,

        CesarPersonalAdministrativo = 119,

        ActualizarAsistenciaPersonal = 120,

        uspAsignarClienteInactivosSinCitaAVendedores = 121,

        ActivarPersonalAdministrativo = 122,

        uspActualizarConfiguracionLogo_adFitness = 123,

        uspRegistrarConfiguracionPagosMensualidades = 124,

        uspRegistrarConfiguracionMatriculas = 125,

        uspRegistrarConfiguracionGastos = 126,

        uspActualizarConfiguracionPagosMensualidadesRecibos = 127,

        uspRegistrarProspectoWeb = 128,

        uspEliminarProspectoWeb = 129,

        uspActualizarProspectoWebASocio = 130,

        ecommerce_uspRegistrar_AspNetUsersTiendaVirtual = 131,

        ecommerce_uspRegistrarComprobante_TiendaVirtual = 132,

        ecommerce_uspRegistrarFormaPago_MercadoPago = 133,

        ecommerce_uspRegistrarFormaPago_Yape = 134,

        ecommerce_uspRegistrarFormaPago_ContraEntrega = 135,

        ecommerce_uspRegistrarPagoComprobante = 136,

        CentroEntrenamiento_uspActualizarMenuPlantillaOrden = 137,

        CentroEntrenamiento_uspEliminarMenuPlantillaPlan = 138,

        CentroEntrenamiento_uspRegistrarMenuPlantillaPlan = 139,

        CentroEntrenamiento_uspRegistrarPerfilMenu = 140,

        CentroEntrenamiento_uspEliminarPerfilMenu = 141,

        CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo = 142,

        CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar = 143,

        CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar = 144,

        CentroEntrenamiento_uspActualizarEdicionPaginaWeb_ColorPrincipalPagina = 145,

        CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL = 146,

        CentroEntrenamiento_uspRegistrarSalaMaquinas_Presencial = 147,

        CentroEntrenamiento_uspEliminarSalaMaquinas_Presencial = 148,

        uspActualizarConfiguracionDatosFormatoTicket = 149,

        uspRegistrarSocios_ImportarExcel = 150,

        UpdateFotoCarnetVacunacion = 151,

        UpdateObligatorioIngresoDNI = 152,

        UpdateMarcarAsistenciaReserva = 153,

        CreateReservarYMarcarAsistencia = 155,
        UpdateObligarMarcarClaseAsistencia = 156,
        UpdatePermitirMuchasAsistenciaPordia = 157,
        UpdateConsultasNumeroDocumentoEntidades = 158,
        uspRegistrarConfiguracionConsultaDocumentoPersonas_Log = 159,
        ecommerce_uspRegistrar_AspNetUsers_AppFitness = 160,
        ecommerce_uspRegistrar_AspNetUsersToken_AppFitness = 161,
        ecommerce_uspValidarCorreo_AspNetUsers_AppFitness = 162,
        appsfit_uspAspNetUsersCentroFit_AgregarFavorito = 163,
        UpdateGenerarCodigoclienteAutomatico = 164,
        UpdateEtapa = 165,
        uspActualizarConfiguracion_HostEnvioEmail = 166,


        UpdateGenerarContratoAutomatico = 167,


        //campaign
        RegisterCamp = 168,
        UpdateCamp = 169,
        DestroyCamp = 170,
        RegisterCampDetail = 171,
        UpdateCampStatu = 178,

        //pasarela empresa
        RegisterPEmpresa = 179,
        UpdatePEmpresa = 180,
        DestroyPEmpresa = 181,

        //ContratoAPi
        RegisterContratoApi = 182,

        //api
           RegisterControlSalidaAPP = 183,
           RegisterSFPagoAPP = 184,

       //api - pago registro
       api_registerComprobante = 185,

        //api - comprobante pago
        RegisterCPApi = 186,

        //email campaing
        UpdateSendCampaing = 187,
        CampaingRegisterFile = 188,
        CampaingDestroyFile = 189,
        CampaingRegisterDetail = 190,

        //notisapp
        NotiAppRegisterDetail = 191,
        NotiAppReadUpdate = 192,

        //suscripcion plan
        PlanSuscriptionRegister = 193,
        PlanSuscriptionDestroy = 194,

    }

    public enum TipoMensaje
    {
        Alerta = 1,
        Informacion = 2,
        Error = 3
    }

    public enum Mes
    {
        Enero = 1,
        Febrero = 2,
        Marzo = 3,
        Abril = 4,
        Mayo = 5,
        Junio = 6,
        Julio = 7,
        Agosto = 8,
        Setiembre = 9,
        Octubre = 10,
        Noviembre = 11,
        Diciembre = 12
    }

    public enum Dia
    {
        Lunes = 1,
        Martes = 2,
        Miercoles = 3,
        Jueves = 4,
        Viernes = 5,
        Sabado = 6,
        Domingo = 7
    }

    public enum Turno
    {
        Dia = 1,
        tarde = 2,
        Noche = 3
    }

    public enum filterCaseEmpresa
    {
        ecommerce_uspListarEmpresas_Paginacion = 1,
        ecommerce_uspBuscarEmpresas = 2,
        ecommerce_uspObtenerEmpresaPorDominio = 3,
        ecommerce_uspObtenerEmpresaPorDominio_AppFitness = 4,
        appsfit_uspAspNetUsersCentroFit_Listar = 5
    }

    public enum filterCaseMaestro
    {
        tabla_ecommerce_TipoDocumentoEmpresa = 1,
        tabla_ecommerce_EstadoEmpresa = 2
    }

    public enum filterCaseCategorias
    {
        ecommerce_uspListarCategorias_Edit = 1,
        ecommerce_uspBuscarCategorias = 2,
        ecommerce_uspBuscarCategoriasTiendaVirutal = 3,
        api_listCategories = 4,
    }

    public enum filterCaseItemsVenta
    {
        ecommerce_uspListarCategorias_Edit = 1,
        ecommerce_uspListarItemsVenta_Paginacion = 2,
        ecommerce_uspBuscarItemsVentas = 3,
        ecommerce_uspBuscadorItemsVentaInventariable = 4,
        ecommerce_uspListarValorInventario_Paginaciones = 5,
        ecommerce_uspListarValorInventario_PuntoVenta = 6,
        ecommerce_uspListarItemsVenta_PorCategoriaPaginacion = 7,
        ecommerce_uspBuscarItemsVentasTienda = 8,
        ecommerce_uspBuscarItemsVentasParaGuardarFoto = 9,
        ecommerce_productbycate = 10,
    }

    public enum filterCaseItemsVentaIncluidosKit
    {
        ecommerce_uspListarCategorias_Edit = 1
    }

    public enum filterCaseItemsVentaInventario
    {
        ecommerce_uspListarCategorias_Edit = 1,
        ecommerce_uspListarMovimientoItemVentaPorItemVenta_Paginaciones = 2
    }

    public enum filterCaseItemsVentaAjusteInventario
    {
        ecommerce_uspListarCategorias_Edit = 1,
        ecommerce_uspListarItemsVentaAjusteInventario_Paginacion = 2
    }

    public enum filterCaseItemsVentaAjusteInventarioDetalle
    {
        ecommerce_uspListarCategorias_Edit = 1,
        ecommerce_uspListarItemsVentaAjusteInventarioDetalle = 1
    }

    public enum filterCaseAlmacenes
    {
        ecommerce_uspListarAlmacenes_Paginacion = 1
    }
    public enum filterCaseComprobante
    {
        ecommerce_uspListarComprobante = 1,
        ecommerce_uspListarComprobanteParaAnular = 2,
        ItemCompCabezeraApp = 3,
    }

    public enum filterCaseFECabecera
    {
        ecommerce_uspListarFECabecera = 1,
        ecommerce_uspListarFECabeceraParaAnular = 2
    }
    public enum filterCaseComprobanteDetalle
    {
        CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_Paginacion = 1,
        CentroEntrenamiento_uspReporteVentasProductosTotalesRangoFechas_NumeroRegistros = 2,
        CentroEntrenamiento_uspListarDeudasCliente = 3,
        CentroEntrenamiento_uspListarComprobanteDetalleParaAnular = 4,
        detalleCompApp = 5
        //ecommerce_uspListarAlmacenes_Paginacion = 1
    }
    public enum filterCaseTipoComprobante
    {
        ecommerce_uspListarTipoComprobante = 1
    }

    public enum filterCaseClientes
    {
        porCodigo = 1,


        BuscarInfoPorCodSocio = 11,


        ListarAgendaSeguimientoPorMes = 14,


        TotalPagosVentas = 19,


        BuscarEstadoCliente = 29,

        ListaInfGeneralPorDiaDeLasSalidas = 30,


        BuscarEstadoInvitado = 40,


        GetCantidadSociosPorVendedor = 42,

        ListarSociosporVencerEnviarCorreo = 43,


        uspListarSocios_PorVendedor_Paginacion = 49,


        uspBuscarSociosConFiltro_Contrato = 57,

        uspBuscarSociosConFiltrosTransferenciaContrato = 58,

        BuscarInfoPorCodSocioFiltro = 59,

        uspInformacionGeneralAbrirCaja = 60,

        uspBuscarClientesDatosAgendaRenovacion = 63,

        uspBuscarClientesDatosAgendaVencidos = 64,

        uspListarClientesActivos = 65,

        uspListarClientesActivos_NumeroRegistros = 66,

        uspListarClientesInactivos = 67,

        uspListarClientesInactivos_NumeroRegistros = 68,

        uspListarClientesPorVencer = 69,

        uspListarClientesPorVencer_NumeroRegistros = 70,

        uspListarClientesPorTodos = 71,

        uspListarClientesPorTodos_NumeroRegistros = 72,

        uspVerMasClientesComprometidosPagosCuotas_Paginacion = 77,

        uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros = 78,

        ListarSociosLibresAsesores_Paginacion = 84,

        uspListarSociosLibresAsesores_NumeroRegistros = 85,

        uspBuscarClientesDatosPorCodigo = 86,


        uspNotificacionCumpleaniosSocios_Paginacion = 89,

        uspNotificacionCumpleaniosSocios_NumeroRegistros = 90,

        uspListarSocios_PorVendedor_NumeroRegistros = 91,

        listaTodosClientesPorTipoAgenda = 92,

        TotalPagosVentasCafeteria = 93,

        uspTotalPagosSuplementosRangoFechas = 94,

        uspListarClientesHombres_MujeresEstadistica = 95,

        uspListarTotalDia_TardeEstadistica = 96,

        uspListarClientesAsistenciaEfectiva_Estadistica = 97,

        uspListarEstadisticaTipoContrato = 98,

        uspListarEstadisticaTiempoMenbresia = 99,

        uspListarClientesAgendaComercialReinscripcion = 100,

        uspListarClientesAgendaComercialReinscripcion_NumeroRegistros = 101,

        uspListarClientesAgendaComercialRenovacion = 102,

        uspListarClientesAgendaComercialRenovacion_NumeroRegistros = 103,

        uspTotalPagosRopasRangoFechas = 104,

        uspListarClientesAgendaComercialRenovacionInscritos = 105,

        uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros = 106,

        uspListarClientesInactivosSinCita = 107,

        uspListarClientesInactivosSinCita_NumeroRegistros = 108,

        uspListarProspectosPostVenta_Paginacion = 109,

        BuscarCodigoDelPrimerSocio = 110,

        uspListarClientesActivos_ExportarExcel = 111,

        uspListarCantidadEstadosClientes = 112,

        uspEstadisticaDashboar = 113,

        uspEstadisticaDashboar_ListadoporvencerExel = 114,

        uspEstadisticaDashboar_ListadoclientesrenovaronExel = 115,

        uspEstadisticaDashboar_ListadoclientesreinscribieronExel = 116,

        uspEstadisticaDashboar_ListadoclientesnuevosExel = 117,

        ecommerce_uspBuscadorClientes = 118,

        CentroEntrenamiento_uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo = 119,

        ecommerce_uspBuscadorClientesPorIdentificacion = 120,

        uspTotalPagosVentasRangoFechas_Appsfit = 121,

        uspTotalVentasTurnos_RangoFechas_Appsfit = 122,

        uspListarVentasTotal = 123,

        uspListarEstadistica_AsistenciaporRangoEdades = 124,

        uspListarEstadistica_AsistenciaporHorarios = 125,

        uspListarEstadistica_AsistenciaporSemana = 126,

        uspCentroEntrenamiento_uspConsumoTotalPorCliente = 127,

        uspCentroEntrenamiento_uspConsumoDetalladoPorCliente = 128
    }

    public enum filterCaseComprobantePago
    {
        ecommerce_uspBuscadorClientes = 1,
        CentroEntrenamiento_uspTotalPagosProductosRangoFechas = 2
    }

    public enum filterCaseAspNetUsers
    {
        ecommerce_AspNetUsers_ValidarUsuarioBusiness = 1,
        ecommerce_uspListarAspNetUsers_Paginacion = 2,
        ecommerce_AspNetUsers_ValidarUsuarioPersonaFit = 3,
        ecommerce_AspNetUsers_ValidarUsuarioPersonaFit_AppFitness = 4,
        ecommerce_AspNetUsers_Buscar = 5,
        ecommerce_uspRecuperarClave_AspNetUsers_AppFitness = 6,
    }


    public enum filterCaseTipoCliente
    {

        porCodigo = 1,

        uspListarTipoClienteAgenda = 2
    }


    public enum filterCaseDisciplina
    {

        porCodigo = 1,

        filter_ListardllDisciplinaProfesor = 2,

        uspListarTotalesPaquetesPorMes = 4
    }



    public enum filterCaseAlimentacionIdealBase
    {

        porCodigo = 1
    }


    public enum filterCaseAlimentacionIdealDetalleBase
    {

        porCodigo = 1,

        ListarAlimentacionIdealDetalleBasePorCodigoAIBase = 2
    }


    public enum filterCaseAlimentacionIdealTips
    {

        porCodigo = 1,

        ListarAlimentacionIdealTipsPorCodigoAIBase = 2
    }




    public enum filterCaseGastos
    {

        porCodigo = 1,

        ListarEgresosPorMesSede = 2,

        ListarEgresosMensuales = 3,

        ListarDetalleEgresosCaja = 4,

        uspReporteEgresoRangoFechas_Paginacion = 5,

        uspReporteEgresoRangoFechas_NumeroRegistros = 6,

        uspReporteEgresoRangoFechas_PaginacionExcel = 7,

        uspReporteEgresoRangoFechas = 8,

        uspReporteEgresoRangoFechas_ExportarExcel = 9,

        ListarEgresosTotal = 10
    }


    public enum filterCaseTipoEgreso
    {

        porCodigo = 1
    }

    public enum filterCaseProductosObservacion
    {

        porCodigo = 1
    }


    public enum filterCaseEmpresaAreas
    {

        porCodigo = 1
    }


    public enum filterCasePagosContrato
    {
        porCodigo = 1,
        ListarPagosFormaPago = 2,
        uspListarPagoMembresia_Anulados = 3
    }


    public enum filterCaseTurnosEmpresa
    {

        porCodigo = 1
    }




    public enum filterCaseUbicaciones
    {

        porCodigo = 1
    }


    public enum filterCaseContratoFolioDTO
    {

        porCodigo = 1
    }



    public enum filterCaseCargos
    {

        porCodigo = 1
    }


    public enum filterCaseColaborador
    {

        porCodigo = 1,


        uspListarProfesores_Visualizar = 3,

        Filter_uspListardllProfesorMenbresia = 4,

        filter_uspBuscarDatosFotoProfesor = 5,

        Filter_uspListarColaboradorHuellero = 6
    }


    public enum filterCaseContrato
    {

        porCodigo = 1,

        porSocio = 2,

        TotalPagosVentas = 3,

        ObtenerTiempoFin = 4,

        porCodigoMembresia = 5,

        porSocioHuellero = 6,

        ListarReporteMembresiasTraspasadas = 7,

        porListarDefault = 8,


        ListarMembresiasSocios = 11,


        ListarMembresiasTraspasoSocios = 13,


        ValidarIngresoDiaPaquete = 15,

        uspListarMembresiasSociosAcuenta_Paginacion = 16,

        uspListarMembresiasSociosAcuenta_NumeroRegistro = 17,

        uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion = 18,

        uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros = 19,

        uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros = 20,

        uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion = 21,

        uspListarMembresiasContrato = 22,

        uspListarMatriculadorAgendaComercial_paginacion = 23,

        uspListarMatriculadorAgendaComercial_NumeroRegistros = 24,

        ExportaruspListarMatriculadorAgendaComercial_paginacion = 25,

        CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo = 26,
        appsfit_uspListarMembresiasSocios = 27
    }



    public enum filterCaseConfiguracionEstadosDelSocio
    {

        porCodigo = 1
    }


    public enum filterCaseHistorialCongelamiento
    {


        ListarHitorialFreezingPorSocio = 2,

        ListarHitorialFreezingPorMenbresia = 3
    }



    public enum filterCasePlanes
    {

        porCodigo = 1,

        ListarporParaMembresia = 2,

        ListarporParaTraspaso = 3,

        uspListarTotalesPaquetesPorMes = 7,

        filter_uspListarPaquetesPorProfesor = 8,

        filter_uspListarPaquetesPorCodigo = 9,


        ListarPaquetesTablaProspectos = 13,

        ListarPaquetesBusquedaFiltroSocio = 14,


        BuscarCantidadCupoPaquetesPorCodigo = 16,

        uspListarPaquetesMenbresiasCursos_Paginacion = 17,

        uspListarPaquetesMenbresiasCursos_NumeroRegistros = 18,
        listApp = 19,
        ListPlanPasarelaByPaquete = 20,

    }


    public enum filterCaseMenu
    {

        porCodigo = 1,

        ListarSEG_PermisosPorCodigoMenuSuperior = 2,

        ListarSEG_PermisosPorCodigoMenu = 3
    }


    public enum filterCasePerfil
    {

        porCodigo = 1,

        filter_uspListarConfiguracionPerfil = 2,

    }


    public enum filterCaseUsuario
    {

        porCodigo = 1,

        ListarVendedor = 2,


        BuscarInfoUsuario = 4,

        ListarUsuariosConFiltroVenta = 5,


        ListarPerfilesFiltro = 7,

        ListarVendedoresMigracion = 8,


        ListarUsuario_ConApertura = 10,

        filterCase_ValidarUsuarioLogeo = 11,

        Filter_uspListarColaboradorHuellero = 12,


        ListarAsesoresVentasAcuentaVentas = 15,

        usplistardllCreadoPor = 16,

        SEGListarUsuario_HacerContrato = 17,

        SEGListarUsuario_AgendaComercial = 18,

        SEGListarUsuarioPorPerfil = 19,

        SEGListarUsuario_TrainnerActivos = 20,

        SEGListarUsuario_NutricionistasActivos = 21,

        SEGListarUsuarioResponsableSuplementos = 22,

        SEGListarUsuarioVendedorPrimerDiaMesConfiguracionMetas = 23,

        uspValidarUsuarioIngresado = 24,

        uspValidarExisteCita_Usuario_AgendaGeneral = 25,

        uspValidarConfiguracionUsuarios = 26,

        Exportar_SEGListarUsuarioPorPerfil = 27

    }


    public enum filterCasePerfilMenu
    {
        //
        // porCodigo = 1,

        SEGListarPerfilMenuPermisos = 2
    }


    public enum filterCaseAsistencia
    {

        porCodigo = 1,

        porDNI = 2,


        porSocioPorCodigo = 5,

        porHoy = 6,

        ListarporMembresia = 7,

        ListarTodosSocios = 8,


        BuscarAsistenciaEfectiva = 11,

        uspListarDetalleAsistenciaSocio_Paginacion = 12,

        uspListarDetalleAsistenciaSocio_NumeroRegistros = 13,

        ListarAsistenciaTodosFiltro_Paginacion = 14,

        uspListar_Socios_Inasistencias_Paginacion = 15,

        uspListar_Socios_Inasistencias_NumeroRegistro = 16,

        uspListarAsistenciaTodosFiltro_NumeroRegistros = 17
    }



    public enum filterCasePedidosVendidos
    {

        porCodigo = 1
    }


    public enum filterCaseContratoCuota
    {

        porCodigo = 1,

        porCodigoMembresia = 2,

        uspListarClientesMenbresiasCuotas = 3
    }


    public enum filterCaseMetas
    {

        porCodigo = 1,

        ListarporFechas = 2,

        ListarporMetas = 3,

        porMeta = 4,

        ListarporVendedores = 5,

        ListarDiasFaltantes = 6,

        ListarporCodigo = 7,

        porResultado = 8,

        ListarMetasExistentes = 9,

        BuscarporImporteAsesor = 10,

        BuscarVentasporDia = 11,

        ListarMetasExistentes_Corporativas = 16,

        uspListarHistorialMetas = 23,

        uspBuscarMetaVendedorPorCodigo = 24,

        uspListarMetasDetalle_VentasAvance = 25,

        uspListarMetasDetalle_EstadisticaVenta = 26,

        uspListarMetasDetalle_CuadroComisiones = 27,

        uspBuscarMetaVendedorPorMesActual = 28,

        uspListarVerificadorCodigosComerciales = 29,

        uspListarVerificadorInformacionSociosComerciales_paginacion = 30,

        uspListarVerificadorInformacionSociosComerciales_NumeroRegistros = 31,

        uspListarEfectivadadCitasVendedores = 32,

        uspListarProductividad_AreaComercial = 33,
        uspListarMetasDetalle_ConversionLeads_Totales = 34,
        uspListarMetricas_ConversionLeads_Totales = 35
    }


    public enum filterCaseComision
    {

        porCodigo = 1,

        Listarporsede_Corporativo = 2,

        porCodigo_Corporativo = 3
    }


    ///filter case de productos

    public enum filterCaseCategoria
    {

        BuscarPorCodigo = 1
    }



    public enum filterCaseMarca
    {

        BuscarPorCodigo = 1
    }


    public enum filterCaseUnidadMedida
    {

        BuscarPorCodigo = 1,

        ListarPorCodigoProveedorProducto = 2,

        ListarPorCodigoProductoVenta = 3
    }


    public enum filterCaseProducto
    {

        BuscarPorCodigo = 1,

        ListarTodo = 2,

        ListaAutoComplete = 3,

        ListaPorNombre = 4,

        ListaPorCategoria = 5,

        ListarImventarioPorNombre = 6,

        BuscarTotalInversion = 7,

        ListarBusquedaPedido = 8,

        uspListarProductosPorFiltro_Paginacion = 9,

        uspListarProductosPorFiltro_NumeroRegistros = 10,

        uspListarProductoPorCategoria = 11,

        uspListarProductoPorCategoriaCompra = 12,

        uspListarProductoBuscadorPorNombre = 13,

        uspListarProductoPorCategoriaVenta = 14,

        uspListarProductoPorCategoriaCompraFiltro = 15,

        uspListarDeudasSuplementoRopaDelSocio = 16,

        uspListarKardexProductos_Paginacion = 17,

        uspListarKardexSuplementos_Paginacion = 18,

        uspListarKardexRopas_Paginacion = 19,

        uspListarHistorialCompraProductos_Paginacion = 20,

        uspListarHistorialCompraProductos_NumeroRegistros = 21,

        uspListarHistorialCompraSuplementos_Paginacion = 22,

        uspListarHistorialCompraSuplementos_NumeroRegistros = 23,

        uspListarHistorialCompraRopas_Paginacion = 24,

        uspListarHistorialCompraRopas_NumeroRegistros = 25
    }


    public enum filterCaseProveedores
    {

        BuscarPorCodigo = 1
    }


    public enum filterCaseCompras
    {

        BuscarPorCodigo = 1,

        BuscarPorNroDocumento = 2,

        ListaPorFecha = 3,
        ListarTodoControlIngreso = 4,

        ListarControlIngresosPorND = 5
    }



    public enum filterCaseComprasDetalle
    {

        ListarCompras = 2,

        ListarComprasEditar = 3,

        uspListarControlDetalleCIngresosRangoFechas = 4,

        uspListarComprasProductos_Paginacion = 5,

        uspListarComprasProductos_NumeroRegistros = 9
    }


    public enum filterCaseTipoDocumento
    {

        BuscarPorCodigo = 1
    }


    public enum filterCaseConfiguracion
    {

        BuscarPorCodigo = 1,

        ListarSedes = 2,

        ValidarTipoMembresiaSede = 3,

        Buscar_RepartirClientes = 4,

        filter_uspBuscarConfVentaOtrosPorCodigo = 5,

        filter_uspBuscarConfCorreoBienvenidaPorCodigo = 6,

        buscarConfiguracionImprimirContrato = 7,

        BuscarConfiguracionAsistencia = 8,

        BuscarConfiguracionTiempoMarcarAsistencia = 9,

        uspBuscarConfiguracionControlPagoSoftware = 10,

        BuscarConfiguracionDiasCitasCaida = 11,

        uspSeguridadObtenerUnidadNegocio = 12,

        uspBuscarInformacionDelUsuario = 13,

        uspListarConfiguracion_apfitness_Paginacion = 14,

        uspListarConfiguracion_apfitness_NumeroRegistros = 15,

        uspBuscarConfiguracion_apfitness = 16,

        uspListaBusquedaClienteContratoAdFitness = 17,

        uspListarSedesPorSedesPermisos = 18,

        uspByteFit_ListarTotalVentasPorEmpresa = 19,

        uspListarConfiguracion_Cobranzas_Paginacion = 20,

        uspListarConfiguracion_Cobranzas_NumeroRegistros = 21,

        uspListarConfiguracionMatriculas = 22,

        uspListarConfiguracionGastos = 23,

        uspListarConfiguracionCuentas = 24,

        uspByteFitMatriculasMensuales = 25,

        uspByteFitVentasResumen = 26,

        uspByteFitVentasMensuales = 27,

        uspByteFitGastosMensuales = 28,

        uspByteFitVentasPorUN = 29,

        uspByteFitClientesNuevosDelMes = 30,

        CentroEntrenamiento_uspBuscarEmpresa_imprimirticket = 31,

        uspSeguridadObtenerUnidadNegocio_SubDominio = 32

    }


    public enum filterCaseVentas
    {

        BuscarPorCodigo = 1,

        ListarPorFecha = 2,

        BuscarInfoGeneralVentaPorCodigo = 3,

        BuscarInformacionDetalleDeVentaPorCodigo = 4,


        uspListarControlSalidaPorFechaAnular_Paginacion = 9,

        uspListarVentasRapidasAnular_Paginacion = 10,

        uspListarControlSalidaPorFechaAnular_NumeroRegistros = 11,

        uspListarVentasRapidasAnular_NumeroRegistros = 12,

        uspListarDeudasSuplementosClientes = 13,

        uspReporteVentasSuplementosTotalesRangoFechas_Paginacion = 14,

        uspListarDeudasRopasClientes = 15,

        uspValidarNroComprobante = 16,

        uspListarCierreMesVentas = 17,

        uspEstadisticaVentasPorEvolucionTicketPromedio_Ventas = 18,

        uspEstadisticaVentasPorTiempoMembresia_Ventas = 19,

        uspEstadisticaVentasPorDiaSemana_Ventas = 20,

        uspEstadisticaVentasPorDia_Ventas = 21,

        uspEstadisticaVentasPorHoras_Ventas = 22,

        uspEstadisticaVentasPorFormaPago_Ventas = 23,

        uspListarEstadistica_VentasDiarios = 24,

        uspEstadisticaMatriculadosPorNombrePlan = 25,
        ventaDiariaByCodigo = 26,

    }


    public enum filterCaseVentasDetalle
    {

        uspReporteVentasRangoFechas_Paginacion = 2,

        uspReporteVentasRangoFechas_NumeroRegistros = 3,

        uspReporteVentasProductosRangoFechas_Paginacion = 4,

        uspReporteVentasProductosRangoFechas_NumeroRegistros = 5,

        uspReporteVentasServiciosRangoFechas_Paginacion = 6,

        uspReporteVentasServiciosRangoFechas_NumeroRegistros = 7,

        uspListarDetalleVentasSuplementos = 8,

        uspReporteVentasCafeteriaRangoFechas_Paginacion = 9,

        uspReporteVentasCafeteriaRangoFechas_NumeroRegistros = 10,

        uspReporteVentasNutricionRangoFechas_Paginacion = 11,

        uspReporteVentasNutricionRangoFechas_NumeroRegistros = 12,

        uspReporteVentasPersonalizadoRangoFechas_Paginacion = 13,

        uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros = 14,

        uspReporteVentasSuplementosTotalesRangoFechas_Paginacion = 15,

        uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros = 16,

        uspListarSuplementosPagosPorFechaAnular_Paginacion = 17,

        uspListarSuplementosPagosPorFechaAnular_NumeroRegistros = 18,

        uspListarDetalleVentasRopas = 19,

        uspListarRopasPagosPorFechaAnular_Paginacion = 20,

        uspListarRopasPagosPorFechaAnular_NumeroRegistros = 21,

        uspReporteVentasRopasTotalesRangoFechas_Paginacion = 22,

        uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros = 23,

        uspReporteVentasMembresuasRangoFechas_PaginacionExcel = 24,

        uspReporteVentasServiciosRangoFechas_PaginacionExcel = 25,

        uspReporteVentasProductosRangoFechas_PaginacionExcel = 26,

        uspReporteVentasSuplementosTotalesRangoFechas_PaginacionExcel = 27,

        uspReporteVentasNutricionRangoFechas_PaginacionExcel = 28,

        uspReporteVentasPersonalizadoRangoFechas_PaginacionExcel = 29,

        uspReporteVentasRopasTotalesRangoFechas_PaginacionExcel = 30,

        uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion = 31
    }



    public enum filterCaseSeries
    {

        BuscarPorCodigo = 1,

        BuscarGenerarCorrelativo = 2
    }

    public enum filterCaseSeriesContrato
    {

        BuscarPorCodigo = 1,

        BuscarGenerarCorrelativo = 2
    }


    public enum filterCaseAgendaSeguimiento
    {

        BuscarPorCodigo = 1,

        ListarAgendaInvitados = 2,

        uspListarGridAgendaGeneral_NumeroRegistros = 3,

        uspListarGridAgendaGeneral_Paginacion = 4,

        ListarInformeCitaVendedores = 5,

        ListarVerSeguimientoAgendaSocios = 6,

        uspListarGridAgendaGeneral_ExportarExcel = 7,

        uspListarGridAgendaGeneralAuditoria_Paginacion = 8,

        uspListarGridAgendaGeneralAuditoria_NumeroRegistros = 9,

        uspListarGridAgendaGeneralAuditoria_TotalTipoActividad = 10,

        uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor = 11,

        uspListarGridAgendaGeneralAuditoria_TotalActividadPorOrigen = 12,
    }


    public enum filterCaseProductoElaborado
    {

        BuscarPorCodigo = 2,

        ListaPorNombre = 1,

        uspListarProductoElaboradoPorFiltro_Paginacion = 5,

        uspListarProductoElaboradoPorFiltro_NumeroRegistros = 6,

        uspListarDiario = 7

    }


    public enum filterCaseReferidosBase
    {


        BuscarPorCodigo = 1,

        BuscarPorMes = 2,

        BuscarPorMesAgenda = 3,

        BuscarPorMesMatriculados = 5
    }


    public enum filterCaseInvitados
    {

        porCodigo = 1,

        ListTodos = 2,

        GetListarTodos_CantidadTotalInvitados = 3,

        BuscarInfoPorCodInvitados = 4,

        ListarTablaInvitados = 5,

        uspBuscarClientesDatosInvitadosPorCodigo = 6,

        uspListarTablaInvitados_Paginacion = 7,

        uspListarTablaInvitados_NumeroRegistros = 8,

        uspBuscarInfoPorCodInvitadoFiltro = 9,

        uspListarInvitadosBusqueda = 10
    }


    public enum filterCasePedidos
    {

    }



    public enum filterCaseTipoContrato
    {

        porCodigo = 1,

        uspBuscarCompromiso = 2,

        uspListarCompromiso = 3,
    }



    public enum filterCaseEnvioCorreo
    {

        porCodigo = 1
    }


    public enum filterCaseSubTipoDocumento
    {

        BuscarPorCodigo = 1,

        ListarPorTipoDocumento = 2
    }


    public enum filterCaseTipoResponsable
    {

        porCodigo = 1
    }



    public enum filterCaseTipoSueldo
    {

        porCodigo = 1
    }



    public enum filterCaseTipoPaquete
    {

        porCodigo = 1,

        FilteruspListaDllTipoPaquete = 2
    }




    public enum filterCaseTablaProspectos
    {

        uspBuscarClientesDatosAgendaRenovacion = 7,

        uspListarTablaProspectos_NumeroRegistros = 8,

        uspListarTablaPropectos_Paginacion = 9,

        uspBuscarClientesProspectosPorCodigo = 10,

        UspListarProspectosSinActividadAgendaComercial = 11,

        UspListarProspectosSinActividadAgendaComercial_NumeroRegistros = 12,
        uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion = 13,
        uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro = 14,
        uspListarProspectosValidadorExisteDNI = 15
    }


    public enum filterCaseAgendaSeguimientoProspecto
    {

        porCodigo = 1
    }


    public enum filterCaseTipoIngreso
    {

        porCodigo = 1

    }


    public enum filterCaseConfiguracionTraspaso
    {

        porCodigo = 1,

        uspBuscar_Configuracion_Congelamiento = 2,

        uspBuscar_Configuracion_Traspaso_Duplicado = 3,

    }


    public enum filterCaseObservacionHN
    {

        porCodigo = 1,

        buscarObservacionPorCodigoHN = 2,

    }



    public enum filterCaseLugaresReservas
    {

        porCodigo = 1,

        Filter_uspBuscarLugaresReservasPorCodigo = 2

    }


    public enum filterCaseMasajista
    {

        porCodigo = 1,

        Filter_uspListarMasajista = 2,

        Filter_uspBuscarMasajistaPorCodigo = 3

    }


    public enum filterCaseEstadoAtencionReservas
    {

        porCodigo = 1,

        Filter_uspListarEstadoAtencionReservas = 2,

        Filter_BuscarPorCodigoEstadoAtencionReservas = 3
    }


    public enum filterCaseHistorialReservas
    {

        Filter_uspListarHistorialReservas = 1,

        Filter_uspBuscarHistorialReservasPorCodigo = 2,

        Filter_uspListarIngresoHistorialReservas = 3,

        Filter_uspListarSalidaHistorialReservas = 4,

        Filter_uspListarHistorialReservasPorMasajista = 5,

        Filter_uspListarTotalHorasHistorialReservasPorMasajista = 6,

        Filter_uspBuscarCantidadSalidaHistorialReservas = 7,

        Filter_uspBuscarCantidadIngresoHistorialReservas = 8
    }


    public enum filterCaseMembresiaCompromiso
    {

        porCodigoMembresia = 1,
    }


    public enum filterCaseLlaves
    {

        Filter_uspListarLlaves = 1,

        Filter_uspBuscarLlavesPorCodigo = 2

    }


    public enum filterCaseComisionMasajistas
    {

        Filter_uspListarComisionMasajistas = 1,

        Filter_uspBuscarComisionMasajistasPorCodigo = 2,

        Filter_uspBuscarTotalComisionPagosPorMasajista = 3

    }


    public enum filterCaseCajaAperturaCierre
    {

        uspBuscarAperturaCaja = 1,

        uspListarAperturaCaja_Paginacion = 2,

        uspBuscarDineroDejadoCajaChica = 3,

        uspBuscarDatosCaja = 4,

        uspInformacionGeneralAbrirCaja = 5,

        uspListarAperturaCaja_NumeroRegistros = 6,

        uspInformacionGeneralAbrirCaja_otrasformaspago = 7
    }



    public enum filterCaseTipoAgendaCliente
    {

        Filter_uspListarTipoAgendaCliente = 1,

        Filter_uspBuscarTipoAgendaClientePorCodigo = 2
    }


    public enum filterCaseTipoSeguro
    {

        uspListarTipoSeguro_Visualizar = 1
    }


    public enum filterCaseGymPerfil
    {

        Filter_uspBuscarGymPerfil = 1
    }

    public enum filterCasePerfilSauna
    {

        Filter_uspBuscarPerfilSauna = 1
    }

    public enum filterCasePerfilBaile
    {

        Filter_uspBuscarPerfilBaile = 1
    }


    public enum filterCaseControlSalidaFormaPago
    {

        Filter_uspListarControlSalidaFormaPago = 1
    }

    public enum filterCaseConfiguracionInnovatec
    {

        Filter_uspBuscarTipoSistema = 1,

    }


    public enum filterCaseHistorialReservasSuit
    {

        Filter_uspListarHistorialReservasSuit = 1,

        Filter_uspBuscarHistorialReservasSuitPorCodigo = 2,

        Filter_uspListarIngresoHistorialReservasSuit = 3,

        Filter_uspListarSalidaHistorialReservasSuit = 4,

        Filter_uspBuscarCantidadSalidaHistorialReservasSuit = 5,

        Filter_uspBuscarCantidadIngresoHistorialReservasSuit = 6
    }


    public enum filterCaseColaboradorLogros
    {

        Filter_usplistarLogroProfesor = 1

    }


    public enum filterCasePuntajesCursos
    {

        Filter_uspBuscarCasePuntajesCursos = 1

    }



    public enum filterCaseCalificacionDesempenioAlumno
    {

        Filter_uspListarCalificacionDesempenioAlumnoPorCodigo = 1
    }


    public enum filterCaseEgresosPaquetes
    {

        porCodigo = 1,

        Filter_uspListarEgresosPaquetesPorMesSede = 2,

        Filter_uspListarEgresosPaquetesPorDia = 3,

        Filter_uspListarEgresosPaquetesPorHorarioClase = 4,

        Filter_uspListarIngresosPaquetes = 5,

        Filter_uspBuscarEgresosPaquetesTotalPorHorarioClase = 6,

        Filter_uspBuscarTotalIngresosPaquetes = 7

    }



    public enum filterCaseHorarioPaqueteDetalle
    {


        Filter_uspListarHoraPaquete = 1,

        uspListarHorasCurso = 2
    }


    public enum filterCaseHorarioPaquete
    {


        uspListarDiasHorarioPaquete_visualizar = 1
    }



    public enum filterCaseEnvioCorreoBienvenida
    {

        uspBuscarEnvioCorreoBienvenidaPorCodigo = 1

    }


    public enum filterCaseRedesSociales
    {

        uspBuscarRedesSocialesPorCodigo = 1
    }




    public enum filterCaseLugaresAlquiler
    {

        uspBuscarLugaresAlquilerPorCodigo = 1
    }
    public enum filterCaseHistorialReservasAlquiler
    {

        uspBuscarHistorialReservasAlquilerPorCodigo = 1,

        Filter_uspListarHistorialReservasAlquiler = 2
    }

    public enum filterCaseContratoMensaje
    {

        ListarMensajesMenbresia = 1

    }


    public enum filterCaseTiempoMembresia
    {


    }
    public enum filterCaseIngresoAjustesCaja
    {

        uspBuscarPorCodigo = 1,

        uspBuscarMontoDeAbrirCaja = 2,

        ListarDetalleAjustesIngresoCaja = 3

    }

    public enum filterCaseConfiguracionGastoApertura
    {

        uspBuscarPorCodigo = 1

    }

    public enum filterCaseAyudaPreguntasFrecuentes
    {

        uspListarAyudaPreguntasFrecuentes = 1
    }

    public enum filterCaseReferido
    {

        uspBuscarPorCodigo = 1,

        uspListarReferido = 2,

        uspListarReferido_Paginacion = 3,

        uspListarTablaReferido_NumeroRegistros = 4
    }

    public enum filterCaseLlamadaEntrante
    {

        uspBuscarPorCodigo = 1,

        uspBuscarClientesDatosAgendaLLamadaEntrante = 2,

        uspListarTablaLlamadaEntrante_Paginacion = 3,

        uspListarTablaLlamadaEntrante_NumeroRegistros = 4,

        uspListarTablaWeb_Paginacion = 5,

        uspListarTablaWeb_NumeroRegistros = 6,

        uspBuscarPropectoWebPorCodigo = 7
    }
    public enum filterCaseBar
    {

        FilterCasePorEmpresa = 1
    }
    public enum filterCaseAsistenciaInvitados
    {

        FilterCaseAsistenciaInvitados = 1,

        uspListarDetalleAsistenciaInvitados_Paginacion = 2,

        uspListarDetalleAsistenciaInvitados_NumeroRegistros = 3,

        ListarAsistenciaTodosFiltroInvitados_Paginacion = 4,

        uspListarAsistenciaTodosFiltroInvitados_NumeroRegistros = 5
    }

    public enum filterCaseWellnessConfiguracion
    {

        FilterCaseListarSauna = 1,

        FilterCaseListarHotel = 2,

        FilterCaseListarBar = 3
    }

    public enum filterCaseMetasDetalle
    {

        uspListarMetasDetalle = 1
    }

    public enum filterCaseAdFitnessAtencionAlCliente
    {

        porCodigo = 1,

        uspListarAdFitnessAtencionAlCliente_Paginacion = 2,

        uspListarAdFitnessAtencionAlCliente_NumeroRegistros = 3
    }

    public enum filterCaseEncuestaNuevo1
    {

        porCodigo = 1,

        uspBuscarEncuestaNuevo1 = 2,

        uspListarEncuestaNuevo2 = 3,

        uspListarEncuestaEstadisticaObjetivos = 4,

        uspListarEstadisticaComoConocioGym = 5,

        uspListarEstadisticaInteres = 6

    }

    public enum filterCaseControlMedidas
    {

        uspListarControlMedidas_Paginacion = 1,

        uspListarControlMedidas_NumeroRegistros = 2,

        uspBuscarControlMedidasPorCodigo = 3,

        uspListarControlMedidasActivas_Paginacion = 4,

        uspListarControlMedidasActivas_NumeroRegistros = 5,

        uspListarControlMedidasInactivas_Paginacion = 6,

        uspListarControlMedidasInactivas_NumeroRegistros = 7,

        uspListarControlMedidasRenovaciones_Paginacion = 8,

        uspListarControlMedidasRenovaciones_NumeroRegistros = 9,

        uspListarControlMedidasSinRutina_Paginacion = 10,

        uspListarControlMedidasSinRutina_NumeroRegistros = 11,

        uspListarAgendaNutricionalGeneralHistorial_Paginacion = 12,

        uspListarAgendaNutricionalGeneralHistorial_NumeroRegistros = 13,

    }

    public enum filterCaseAgendaNutricional
    {

        uspListarAgendaNutricionalGeneral_Paginacion = 1,

        uspBuscarAgendaNutricionalPorCodigo = 3,

        uspListarAgendaNutricionalPorCliente = 2,

        uspListarAgendaNutricionalGeneral_NumeroRegistros = 4,

        uspValidarHorariosOcupadosCitasNutricionales = 5
    }

    public enum filterCaseSuplementos
    {

        porCodigo = 1,

        uspListarSuplementosPorFiltro_Paginacion = 2,

        uspListarSuplementosPorFiltro_NumeroRegistros = 3,

        uspListarSuplementos = 4,

        uspListarSuplementosPorCategoria = 5,

        uspListarSuplementosComprasPorCategoria = 6,

        uspListarSuplementosVentasPorCategoria = 7,

        uspListarSuplementosComprasPorCategoriaFiltro = 8
    }

    public enum filterCaseCategoriaSuplemento
    {

        porCodigo = 1
    }

    public enum filterCaseComprasSuplementos
    {

        porCodigo = 1,

        uspListarComprasSuplementos_Paginacion = 2,
        uspListarComprasSuplementos_NumeroRegistros = 3
    }

    public enum filterCasePagosSuplementos
    {

        porCodigo = 1,

        uspListarPagosSuplementosPorCodigoSalida = 2,

        uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion = 3,

        uspListarDeudasSuplementosTotalesDiaRangoFechas_NumeroRegistros = 4,

        uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion = 5,

        uspListarACuentaSuplementosPorFechaRangoFechas_NumeroRegistros = 6

    }

    public enum filterCaseProfesionalFitness
    {

        uspBuscarProfesionalFitnessPorCodigo = 1,

        BuscarNumeroDocumento = 2,

        BuscarPorNombres = 3
    }

    public enum filterCaseUsuariosIngresos
    {

        PorCodigo = 1,

        uspValidarAccesoSistema = 2
    }

    public enum filterCaseHorarioPersonalEventuals
    {

        PorCodigo = 1
    }
    public enum filterCaseInteresProspectos
    {

        PorCodigo = 1
    }
    public enum filterCaseRopas
    {

        PorCodigo = 1,

        uspListarRopasPorFiltro_Paginacion = 2,

        uspListarRopasPorFiltro_NumeroRegistros = 3,

        uspListarRopasCompras = 4,

        uspListarRopas = 5,

        uspListarRopasVentas = 6,

        uspListarRopasComprasFiltro = 7
    }

    public enum filterCaseComprasRopas
    {

        PorCodigo = 1,

        uspListarComprasRopas_Paginacion = 2,

        uspListarComprasRopas_NumeroRegistros = 3

    }

    public enum filterCasePagosRopas
    {

        PorCodigo = 1,

        uspListarPagosRopasPorCodigoSalida = 2,

        uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion = 3,

        uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros = 4

    }


    public enum filterCaseHorarioClases
    {

        PorCodigo = 1,

        ListarTodos = 2,

        ListarCalendarioDiario = 3,

        ListarPorProfesional = 4,

        ListarPorFecha = 5,

        BuscarPorCodigoConDetalle = 6
    }

    public enum filterCaseConfiguracionHorarioClases
    {

        PorCodigo = 1,

        ListarTodos = 2
    }

    public enum filterCaseHorarioClasesDetalle
    {

        PorCodigo = 1,

        ListarTodos = 2,

        ListaCalendario = 3,

        InformacionSocio = 4
    }

    public enum filterCaseSalaHorario
    {

        PorCodigo = 1,

        ListarTodos = 2
    }

    public enum filterCaseHorarioClasesConfiguracion
    {

        PorCodigo = 1,

        ListarTodos = 2,

        Calendario = 3,

        uspListarHorarioClasesConfiguracionCalendario_ExportarExcel = 4
    }

    public enum filterCasePersonalAdministrativo
    {

        PorCodigo = 1,

        BuscarAsistenciaConfiguracionPorNumeroDocumento = 2,

        ListarPorFiltros = 3,

        BuscarPorNumeroDocumentoGlobal = 4
    }

    public enum filterCasePersonalAsistencia
    {

        AsistenciaProfesores = 1,

        AsistenciaPersonalAdministrativo = 2,

        ListaAsistenciaPorCodigoPersonal = 3,

        BuscarAsistenciaPorDNI = 4,

        FilterAutocomplete = 5,

        ListarTodasAsistenciaPorDNI = 6,

        ListarAsistenciaPersonalAdministrativoResumen = 7
    }

    public enum filterCasePersonalAsistenciaConfiguracion
    {

        BuscarPorCodigo = 1
    }

    public enum filterCasePersonalAsistenciaNota
    {

        BuscarPorCodigo = 1
    }

    public enum filterCaseSala
    {

        PorCodigo = 1
    }

    public enum filterCaseAgendaPostVenta
    {

        uspListarAgendaPostVentaSeguimiento_NumeroRegistros = 1,

        uspListarAgendaPostVentaSeguimiento_Paginacion = 2,

        uspListarCalificacionPostVenta = 3,

        uspListarTipoAgendaPostVenta = 4,


        uspListarAgendaPostVentaSeguimientoSocios_Mensajes = 5,

        uspListarAgendaPostVentaSeguimientoReferido_Mensajes = 6,

        uspListarAgendaPostVentaSeguimientoProspectos_Mensajes = 7,

        uspListarAgendaPostVentaSeguimientoLlamadaEntrante_Mensajes = 8,

        uspListarAgendaPostVentaSeguimientoInvitado_Mensajes = 9,

        uspListarCantidadSeguimiento = 10
    }

    public enum filterCaseSociosFichaSaludMaster
    {

        ListarSociosFichaSaludMaster = 1
    }

    public enum filterCaseEnvioMensajeGP
    {

        ListarSociosFichaSaludMaster = 1
    }

    public enum filterCaseCentroEntrenamiento_Presencial_Sala
    {
        CentroEntrenamiento_uspListarSala_Presencial = 1,
        CentroEntrenamiento_uspListarSalaMaquinas_Presencial = 2
    }

    public enum filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion
    {
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario = 1,
        CentroEntrenamiento_uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo = 2,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb = 3,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS = 4,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS = 5,
        CentroEntrenamiento_uspObtenerFechasReservas_Configuracion = 6,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion = 7,
        CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS = 8,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros = 9,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas = 10,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS = 11,
        CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion = 12,
        CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros = 13,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionChecking = 14,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES = 15,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros = 16,
        CentroEntrenamiento_uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE = 17,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy = 18,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendarioChecking = 19
    }

    public enum filterCaseCentroEntrenamiento_Presencial_DisciplinaSala
    {
        CentroEntrenamiento_uspListarDisciplinaSala_Presencial = 1
    }

    public enum filterCaseCentroEntrenamiento_Profesor
    {
        CentroEntrenamiento_uspBuscarProfesorPorNombres = 1,
        CentroEntrenamiento_uspListarPresencial_uspListarPersonalClasesGrupales = 2
    }
    public enum filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias
    {
        CentroEntrenamiento_uspBuscarProfesorPorNombres = 1,
        CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio = 2,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion = 3,
        CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking = 4
    }
    public enum filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness
    {
        CentroEntrenamiento_uspListarPresencial_ConfiguracionSalaFitness = 1,
        CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal = 2,
        CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion = 3,
        CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion = 4
    }

    public enum filterCaseAspNetUsersDireccionesEntrega
    {
        ecommerce_uspListarAspNetUsers_DireccionesEntrega = 1
    }

    public enum filterCaseCupones
    {
        ecommerce_uspListar_Cupones = 1,
        ecommerce_uspBuscar_Cupones = 2,
        ecommerce_uspBuscar_CuponesXCodigoPromocion = 3
    }

    public enum filterCaseTarifasEnvio
    {
        ecommerce_uspListarAdminTarifasEnvio = 1
    }
    public enum filterCasePlantillaFormaPago
    {
        ecommerce_uspListarAdminFormaPago = 1,
        ecommerce_uspBuscarFormaPago_MercadoPago = 2,
        ecommerce_uspBuscarFormaPago_Yape = 3,
        ecommerce_uspBuscarFormaPago_ContraEntrega = 4,
        listTypesPasarela = 5,
    }

    public enum filterCaseEnvioGratis
    {
        ecommerce_uspBuscarEnvioGratis = 1,
        ecommerce_uspBuscarEnvioGratisXtienda = 2
    }

    public enum filterCaseCentroEntrenamiento_EditorPaginaWeb
    {
        CentroEntrenamiento_uspBuscarEdicionPaginaWeb_BannerReserva = 1,
        uspBuscarLogoCorporativo = 2
        //ecommerce_uspBuscarEnvioGratisXtienda = 2
    }

    public enum filterCaseCentroEntrenamiento_EditorPaginaWebDetalle
    {
        ecommerce_uspBuscarEdicionPaginaWebDetalle = 1,
        ecommerce_uspListarEdicionPaginaWebDetalle = 2
    }
    public enum filterCaseCentroEntrenamiento_GaleriaFitness
    {
        CentroEntrenamiento_uspListarGaleriaFitness = 1
    }

    public enum filterCaseCentroEntrenamiento_MenuPlantilla
    {
        CentroEntrenamiento_uspListarMenuPlantilla = 1,
        CentroEntrenamiento_uspBuscarMenuPlantilla = 2,
        CentroEntrenamiento_uspListarSEG_Planes = 3,
        CentroEntrenamiento_uspListarMenuPlantillaPlan = 4,
        SEGListarPerfilMenu = 5
    }

    public enum filterCasegimnasio_crm_1_embudosventaplantilla
    {
        uspListar_gimnasio_crm_1_embudosventaplantilla = 1,
        uspBuscar_gimnasio_crm_1_embudosventaplantilla = 2
    }
    public enum filterCasegimnasio_crm_2_etapasplantilla
    {
        CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla = 1,
        CentroEntrenamiento_uspBuscar_gimnasio_crm_2_etapasplantilla = 2
    }

    public enum filterCasegimnasio_crm_3_tratosprospecto
    {
        CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto = 1,
        CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto = 2,
        CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto = 3,
        CentroEntrenamiento_uspListar_gimnasio_crm_4_etapahistorial = 4
    }

    public enum filterCaseCentroEntrenamiento_AspNetUsers
    {
        CentroEntrenamiento_uspBuscarAspNetUsers_imprimirticket_DefaultKey = 1
    }


    public enum filterCaseNotificacionApp
    {
        BuscarPorCodigo = 1,
        Listar = 2,
        ListActive = 3,
        ListByVencer = 4,
        ListVencids = 5,
        listAddressPaginate = 6,
        listNotisByUser = 7,
    }
    public enum FilterCaseWhatsapp
    {
        SearchByCode = 1,
        List = 2,
    }
    public enum FilterCaseGlobal
    {
        SearchByCode = 1,
        List = 2, 
        SearchByCodeCamp = 3,
        ListCamp = 4,
        ListCampDetail = 5,
    } 
    
    public enum FilterCasePasarelaEmpresa
    {
        SearchByCode = 1,
        List = 2,
        ListActive = 3,
        ListApi = 4,
        SearchCodeApi = 5,
    }
    public enum FilterEmailCampaing
    {
        ListPagination = 1,
        Search = 2,
        ListFiles = 3,
        ListPaginationDetail = 4,
    }
}
