<%@ Page Title="Post Classified" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="PostClassified.aspx.vb" Inherits="AspNetPortal.PostClassified" ValidateRequest="false" %>
<%@ Import Namespace="AspNetPortal" %>
<%@ Import Namespace="Microsoft.AspNet.Identity" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <table width="100%"><tr><td><h2><%: Title %></h2></td><td align="right"><h4><b>Hello <%: Context.User.Identity.GetUserName() %>!</b></></h4></td></tr></table>
    <hr>

    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
        <p class="text-danger"><asp:Literal runat="server" ID="FailureText" /></p>
    </asp:PlaceHolder>


    <table Name="PostArea" width="100%" cellspacing="10">
        <tr>
            <td valign="top" width="75%">

                <h4>Title/Subject: (required)</h4>
                <textarea rows="1" maxlength="255" id="pTitle" name="pTitle" style="width: 100%; font-family:Verdana, Geneva, Tahoma, sans-serif; font-size: x-large; " required></textarea>
                
                <br /><br />
                <h4>Content/Description: (required)</h4>
                <textarea id="pContent" name="pContent" style="width: 100%; height: 400px;" rows="20" cols="70" required></textarea>

                <table width="100%">
                    <tr>
                        <td><asp:label ID="pQty"  runat="server" Visible="true" Text="Quantity: (Optional)" /></td>
                        <td><input id="pQuantity" type="number" min="0" max="99999" /></td>
                        <td><asp:label ID="Price" runat="server" Visible="true" Text="Price: (Optional)" /></td>
                        <td><input id="pPrice" type="number"  min="0.00" max="99999.99" /></td>
                    </tr>
                </table>

                <h4>SEO Description: (optional)</h4>
                <textarea rows="2" maxlength="180" id="pSeoDesc" name="pSeoDesc" style="width: 100%;"></textarea>
                
                <br /><br />
                <h4>SEO Abstract: (optional)</h4>
                <textarea rows="4" maxlength="500" id="pSeoAbs" name="pSeoAbs" style="width: 100%;"></textarea>
            </td>
            <td>&nbsp;&nbsp;</td>

            <td valign="top">

                <h4>Contact Information:</h4>
                <table width="100%">
                    <tr>
                        <td>Full Name: (required)</td>
                    </tr>
                    <tr>
                        <td><input type="text" id="pContact" name="pContact" maxlength="75" style="width: 100%;" required></td>
                    </tr>
                    <tr>
                        <td>Full Address: (optional)</td>
                    </tr>
                    <tr>
                        <td><textarea id="pAddress" name="pAddress" maxlength="255" style="width: 100%;" rows="4"></textarea></td>
                    </tr>

                    <tr>
                        <td>E-mail Address: (required)</td>
                    </tr>
                    <tr>
                        <td><input type="email" id="pEmail" name="pEmail" maxlength="75" style="width: 100%;" required></td>
                    </tr>
                    <tr>
                        <td>Phone/Cell Number: (required)</td>
                    </tr>
                    <tr>
                        <td><input type="tel" id="pPhone" name="pPhone" maxlength="75" style="width: 100%;" required></td>
                    </tr>
                    <tr>
                        <td>Webpage URL: (optional)</td>
                    </tr>
                    <tr>
                        <td><input type="url" id="pLink" name="pLink" maxlength="75" style="width: 100%;"></td>
                    </tr>
                </table>

                <br />
                <h4>Status:</h4>
                <table width="100% cellspacing="2">
                <tr><td nowrap>Publish Immediately:</td><td>&nbsp;&nbsp;</td><td><select style="width:60px;" id="pIsPublished" name="pIsPublished">
                <option value="0">No</option>
                <option value="1" selected>Yes</option>
                </select></td></tr>
                <tr><td>Publish on Twitter:</td><td>&nbsp;&nbsp;</td><td><select  style="width:60px;" id="IsTwittered" name="pIsTwittered">
                <option value="0">No</option>
                <option value="1" selected>Yes</option>
                </select></td></tr>
                <tr><td>Allow comments:</td><td>&nbsp;&nbsp;</td><td><select style="width:60px;" id="pIsCommented" name="pIsCommented">
                <option value="0">No</option>
                <option value="1" selected>Yes</option>
                </select></td></tr>
                <tr><td>Allow notifications:</td><td>&nbsp;&nbsp;</td><td><select style="width:60px;" id="pIsNotified" name="pIsNotified">
                <option value="0">No</option>
                <option value="1" selected>Yes</option>
                </select></td></tr>
                <tr><td>Add my signature:</td><td>&nbsp;&nbsp;</td><td><select style="width:60px;" id="pIsSigned" name="pIsSigned">
                <option value="0">No</option>
                <option value="1" selected>Yes</option>
                </select></td></tr>
                <tr><td></td><td>&nbsp;&nbsp;</td><td></td></tr>
                </table>

                <input id="Submit" name="Submit" type="submit" runat="server" value="ADD THIS CLASSIFIED NOW" class="redbtn btn-default" style="width:100%;" onclick="AddClassified_Click" />
                <hr />
                <p>In the next step, you will be able to assign this classified to the right region (country,state,city), to the right category and subcategory, add pictures, re-edit it, etc.</p>
            </td>
        </tr>
    </table>

    <div class="row">
    </div>

    <script>
        tinymce.init({
            selector: '#Content',
            menu: {
                file: { title: 'File', items: 'newdocument restoredraft | preview | print ' },
                edit: { title: 'Edit', items: 'undo redo | cut copy paste | selectall | searchreplace' },
                view: { title: 'View', items: 'code | visualaid visualchars visualblocks | spellchecker | preview fullscreen' },
                insert: { title: 'Insert', items: 'image link media template codesample inserttable | charmap emoticons hr | pagebreak nonbreaking anchor toc | insertdatetime' },
                format: { title: 'Format', items: 'bold italic underline strikethrough superscript subscript codeformat | formats blockformats fontformats fontsizes align lineheight | forecolor backcolor | removeformat' },
                tools: { title: 'Tools', items: 'spellchecker spellcheckerlanguage | code wordcount' },
                table: { title: 'Table', items: 'inserttable | cell row column | tableprops deletetable' },
                help: { title: 'Help', items: 'help' }
            },
            toolbar: 'undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | cut copy paste | outdent indent | link image video',
            plugins: [
                'advlist autolink link image lists charmap print preview hr anchor pagebreak',
                'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
                'table emoticons template paste help'
            ],
        });
    </script>

</asp:Content>
