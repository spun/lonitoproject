﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="contacto.aspx.cs" Inherits="WEvents4ALL.contacto"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Content1" runat="server">
    <h3>Formulario de Contacto</h3>

    <br />
    <div class="contacto_cuerpo">
        <label class="contact_label">Mail</label><asp:TextBox ID="TextBox1" runat="server" CssClass="contact_imput_down"></asp:TextBox>        
        <span class="rank_help-inline2"> *Mail invalido</span><br />
        <label class="contact_label">Asunto</label><asp:DropDownList ID="contacto_DropDownList1" runat="server" CssClass="contact_imput_down">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem >Error en web</asp:ListItem>
                    <asp:ListItem >Ventas</asp:ListItem>
                    <asp:ListItem >Otros</asp:ListItem>
        </asp:DropDownList>
        <span class="rank_help-inline"> *Debe seleccionar un asunto</span>
        <br />
        <label>Motivo</label><br />
        <textarea id="TextArea1" rows="30" cols="80" class="contact_textarea" runat=server></textarea>
     </div><br />
        <asp:Button ID="bot_enviar" runat="server" Text="Enviar" 
        CssClass="contact_bot_enviar" OnClick="EventoClick" />
        
        <script>
            $(document).ready(function () {
                $('.rank_help-inline').hide();
                $('.rank_help-inline2').hide();
                $('#form1').on('submit', function (e) {
                    console.log('yes');
                    $('.rank_help-inline').hide();
                    $('.rank_help-inline2').hide();
                    $('.contacto_cuerpo').removeClass('error');
                    if (validarContacto() == false)
                        e.preventDefault();
                });
            });

	        function validarContacto() {
	            var correcto = true;

	            //validacion que fuerza a que se seleccione un tema
	            var asunto = $('#Content1_contacto_DropDownList1');
	            var mail = $('#Content1_TextBox1');
	            var regexMail = /^(.+\@.+\.[a-z]{2,4})$/;
	            if (asunto.val() == "") {
	                asunto.parents('div.contacto_cuerpo').addClass('error').find('.rank_help-inline').show();
	                correcto = false;
	            }
	            if (mail.val() == "" || !regexMail.test(mail.val())) {
	                asunto.parents('div.contacto_cuerpo').addClass('error').find('.rank_help-inline2').show();
	                correcto = false;
	            }

	            return correcto;
	        }				
	</script>
</asp:Content>
