<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ES_Eventos_web_app.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title></title>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>

    <style>
        .App-logo {
            animation: App-logo-spin infinite 0.9s linear;
            height: 10vmin;
            pointer-events: none;
        }
        .App-logo-inverse {
            animation: App-logo-spin infinite 0.9s linear;
            height: 10vmin;
            pointer-events: none;
        }
        @keyframes App-logo-spin {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }
        @keyframes App-logo-spin-inverse {
            from {
                transform: rotate(0deg);
            }

            to {
                transform: rotate(360deg);
            }
        }
    </style>
</head>
<body style="font-family: Arial, Helvetica, sans-serif; font-size: small; padding-top: 125px;">
    <nav class="navbar fixed-top navbar-expand-lg navbar-dark bg-primary" id="navbar1">
        <div class="container">
            <img class="img-fluid" style="width:85px;height:85px;" src="~/res/icon.jpeg" alt="Logo"/>
        </div>
    </nav>
    <div class="d-flex justify-content-center align-items-center">
        <div class="container body-content">
           <h1 class="text-center">Display 4</h1>
           <form id="form1" runat="server">
              <div>
                 <h4 style="font-size: medium">Log In</h4>
                 <hr />
                 <asp:PlaceHolder runat="server" ID="LoginStatus" Visible="false">
                    <p>
                       <asp:Literal runat="server" ID="StatusText" />
                    </p>
                 </asp:PlaceHolder>
                 <asp:PlaceHolder runat="server" ID="LoginForm" Visible="false">
                    <div class="form-group" style="margin-bottom: 10px">
                       <asp:Label class="label" runat="server" AssociatedControlID="UserName">User name</asp:Label>
                       <div>
                          <asp:TextBox class="form-control" runat="server" ID="UserName" />
                       </div>
                    </div>
                    <div class="form-group" style="margin-bottom: 10px">
                       <asp:Label class="label" runat="server" AssociatedControlID="Password">Password</asp:Label>
                       <div>
                          <asp:TextBox class="form-control" runat="server" ID="Password" TextMode="Password" />
                       </div>
                    </div>
                    <div class="form-group" style="margin-bottom: 10px">
                       <div>
                          <asp:Button class="btn btn-primary" runat="server" OnClick="SignIn" Text="Log in" />
                       </div>
                    </div>
                 </asp:PlaceHolder>
                 <asp:PlaceHolder runat="server" ID="LogoutButton" Visible="false">
                    <div>
                       <div>
                          <asp:Button class="btn btn-primary" runat="server" OnClick="SignOut" Text="Log out" />
                       </div>
                    </div>
                 </asp:PlaceHolder>
              </div>
           </form>
        </div>
    </div>
</body>
</html>
