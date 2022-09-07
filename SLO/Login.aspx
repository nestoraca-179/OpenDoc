<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SLO.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login | SDO</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-gH2yIJqKdNHPEq0n4Mqa/HGKIhSkIHeL5AyhkYV8i59U5AR6csBvApHHNl/vI1Bx" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.2/css/all.min.css" integrity="sha512-1sCRPdkRXhBV2PBLUdRb4tMg1w2YPf37qatUFeS7zlBy7jJI8Lf4VHwWfZZfpXtYSLy85pkm9GaYVYMfw5BC1A==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link href="/Content/Site.css" rel="stylesheet" />
<style>
    .container-fluid {
        width: 100%;
        height: 100%;
        background: #d5d5db;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    form {
        width: 100%;
        height: 600px;
        max-width: 500px;
        background: #FFF;
        padding: 15px;
        display: flex;
        flex-direction: column;
        justify-content: center;
    }
    .title-login {
        flex: 1;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .credentials-login {
        flex: 4;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
    .form-group {
        width: 80%;
        display: flex;
        flex-wrap: wrap;
    }
    .form-group i {
        flex: 1;
        color: #333;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .form-group input {
        flex: 10;
    }
    .form-control-sm {
        margin: 5px 0;
        padding: 10px 0;
        border: 0;
        border-bottom: solid 1px #c1c1c1;
        border-radius: 0;
    }
    .form-label-error {
        width: 100%;
        font-size: 12px;
        color: #ca1d1d;
        display: none;
    }
    #LBL_Error.form-label-error {
        display: block;
    }
</style>
</head>
<body>
    <div class="container-fluid">
        <form id="form1" runat="server" class="shadow-tm">
            <div class="title-login">
                <h1 class="text-center">SDO</h1>
            </div>
            <div class="credentials-login">
                <div class="form-group">
                    <i class="fas fa-user"></i>
                    <asp:TextBox ID="TB_Username" runat="server" Width="100%" CssClass="form-control-sm" placeholder="Usuario" ClientIDMode="Static" onkeypress="$('.form-label-error').css('display', 'none');"></asp:TextBox>
                    <asp:Label Text="* Debes ingresar usuario" runat="server" CssClass="form-label-error" />
                </div>
                <div class="form-group">
                    <i class="fas fa-lock"></i>
                    <asp:TextBox ID="TB_Password" runat="server" Width="100%" CssClass="form-control-sm" placeholder="Clave" ClientIDMode="Static" onkeypress="$('.form-label-error').css('display', 'none');" MaxLength="15" TextMode="Password"></asp:TextBox>
                    <asp:Label Text="* Debes ingresar clave" runat="server" CssClass="form-label-error" />
                </div>
                <asp:Button ID="BTN_Login" runat="server" Text="Iniciar Sesión" CssClass="btn btn-primary mt-3" Width="80%" OnClick="Btn_Login_Click" OnClientClick="return checkForm()"></asp:Button>
                <asp:Label ID="LBL_Error" runat="server" Visible="false" CssClass="form-label-error mt-2 text-center" Width="80%" ClientIDMode="Static"></asp:Label>
            </div>
        </form>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-A3rJD856KowSb7dwlZdYEkO39Gagi7vIsF0jrRAoQmDKKtQBHUuLZ9AsSv4jD4Xa" crossorigin="anonymous"></script>
    <script>
        function checkForm() {

            var send = true;

            if ($("#TB_Username").val() == "") {
                $(".form-label-error").first().css("display", "block");
                send = false;
            }

            if ($("#TB_Password").val() == "") {
                $(".form-label-error").last().css("display", "block");
                send = false;
            }

            return send;
        }
    </script>
</body>
</html>