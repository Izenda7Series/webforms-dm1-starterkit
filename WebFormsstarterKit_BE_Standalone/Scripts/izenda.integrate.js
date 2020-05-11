var DoIzendaConfig = function () {
    var hostApi = "http://localhost:5101/api/";
    var configJson = {
        "WebApiUrl": hostApi,
        "BaseUrl": "/izenda",
        "RootPath": "/Scripts/izenda",
        "CssFile": "izenda-ui.css",
        "Routes": {
            "Settings": "settings",
            "New": "new",
            "Dashboard": "dashboard",
            "Report": "report",
            "ReportViewer": "reportviewer",
            "ReportViewerPopup": "reportviewerpopup",
            "Viewer": "viewer"
        },
        "OnReceiveUnauthorizedResponse": redirectToLoginPage,
        "Timeout": 3600
    };
    IzendaSynergy.config(configJson);
};

function errorFunc() {
    // confirm dialog
    alertify.confirm("Your token was not generated correctly, please login.", function () {
        // user clicked "ok"
    }, function() {
        // user clicked "cancel"
    });
}

function redirectToLoginPage() {
    alert("You're unauthorized to access Izenda page");
    location.href = "/Account/Login";
}

var RecycleComponent = function (componentID) {
    var component = document.getElementById(componentID);
    IzendaSynergy.unmountComponent(component);

    var baseUrl = window.location.href.split('#')[0]; //removes the anchor from the URL for the window history
    window.history.pushState({ 'html': document.documentElement.outerHTML }, '', baseUrl); //sets URL to initial state (no Izenda routing)

    component.innerHTML = '';

};

var DoRender = function (successFunc) {
    var tokenApi = "/api/user/GenerateToken";
    $.ajax({
        type: "GET",
        url: tokenApi,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
};

var izendaInit = function () {
    function successFunc(data, status) {
        var currentUserContext = {
            token: data
        };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.render(document.getElementById('izenda-root'));
    }

    this.DoRender(successFunc);

};

var izendaInitReport = function () {

    function successFunc(data, status) {
        var currentUserContext = {
            token: data
        };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderReportPage(document.getElementById('izenda-root'));
    }

    this.DoRender(successFunc);

};

var izendaInitSetting = function () {

    function successFunc(data, status) {
        console.info(data);
        var currentUserContext = {
            token: data
        };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderSettingPage(document.getElementById('izenda-root'));
    }

    this.DoRender(successFunc);
};

// Render report parts to specific <div> tag by report part id
var izendaInitReportPartDemo = function () {

    function successFunc(data, status) {
        console.info(data);
        var currentUserContext = {
            token: data
        };


        // You can add report parts after creating reports using the context below 
        // Add the report part ID's in the <reportPartIds> array
        var reportPartIds = new Array(
                "[your 1st report part id]",
                "[your 2nd report part id]",
                "[your 3rd report part id]",
                "[your 4th report part id]"
            );

        IzendaSynergy.setCurrentUserContext(currentUserContext);

        for (var i = 0; i < reportPartIds.length; i++) {
            //create report part container
            var reportPartNode = document.createElement("div");
            reportPartNode.className = "report-part-chart";

            //Render report part
            IzendaSynergy.renderReportPart(reportPartNode, { "id": reportPartIds[i] });

            //Append to izenda-root container
            document.getElementById('izenda-root').appendChild(reportPartNode);
        }
    }
    this.DoRender(successFunc);
};

var izendaInitReportPartUpdateResult = function (reportPartId, overridingFilterValue, container) {

    function successFunc(data, status) {
        console.info(data);
        var currentUserContext = {
            token: data
        };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderReportPart(document.getElementById(container), {
            "id": reportPartId,
            "overridingFilterValue": overridingFilterValue,
        });
    }

    this.DoRender(successFunc);
};

var izendaRenderReportPart = function (reportPartId, container) {

    function successFunc(data, status) {
        console.info(data);
        var currentUserContext = {
            token: data
        };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderReportPart(document.getElementById(container), {
            "id": reportPartId
        });
    }

    this.DoRender(successFunc);
};

var izendaInitReport = function () {

    function successFunc(data, status) {
        var currentUserContext = {
            token: data
        };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderReportPage(document.getElementById('izenda-root'));
    }

    this.DoRender(successFunc);

};

// Render report viewer to a <div> tag by report id
var izendaInitReportViewer = function (reportId) {
    function successFunc(data, status) {
        var currentUserContext = {
            token: data
        };
        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderReportViewerPage(document.getElementById('izenda-root'), reportId);
    }

    this.DoRender(successFunc);

};

var izendaInitDashboard = function () {

    function successFunc(data, status) {
        var currentUserContext = {
            token: data
        };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderDashboardPage(document.getElementById('izenda-root'));
    }

    this.DoRender(successFunc);

};

var izendaInitReportDesigner = function () {

    function successFunc(data, status) {
        var currentUserContext = {
            token: data
        };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderReportDesignerPage(document.getElementById('izenda-root'));
    }

    this.DoRender(successFunc);

};

var izendaInitNewDashboard = function () {

    function successFunc(data, status) {
        var currentUserContext = {
            token: data
    };

        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderNewDashboardPage(document.getElementById('izenda-root'));
    }

    this.DoRender(successFunc);

};

// Render dashboard viewer to a <div> tag by dashboard id
var izendaInitDashboardViewer = function (dashboardId) {
    function successFunc(data, status) {
        var currentUserContext = {
            token: data
        };
        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderDashboardViewerPage(document.getElementById('izenda-root'), dashboardId);
    }

    this.DoRender(successFunc);
};


// Render report part
var izendaInitReportPartViewer = function (reportPartId) {
    function successFunc(data, status) {
        var currentUserContext = {
            token: data
        };
        IzendaSynergy.setCurrentUserContext(currentUserContext);
        IzendaSynergy.renderReportPart(document.getElementById('izenda-root'), {
            id: reportPartId
        });
    }

    this.DoRender(successFunc);

};

var izendaInitReportPartExportViewer = function (reportPartId, token) {
    var currentUserContext = {
        token: token
    };
    IzendaSynergy.setCurrentUserContext(currentUserContext);
    IzendaSynergy.renderReportPart(document.getElementById('izenda-root'), {
        id: reportPartId,
        useQueryParam: true,
        useHash: false
    });
};
