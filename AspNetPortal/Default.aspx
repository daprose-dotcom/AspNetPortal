<%@ Page Title="Home" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="AspNetPortal._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <nav aria-label="breadcrumb">
      <ol class="breadcrumb">
        <li class="breadcrumb-item" aria-current="page"><a runat="server" href="~/"> All Categories </a></li>
      </ol>
    </nav>

    <div class="row">

        <div class="col-md-4">
            <asp:SqlDataSource id="SqlDataSource1" runat="server"/>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="CAT_ID" Width="100%" DataSourceID="SqlDataSource1" CellPadding="2" ForeColor="#333333" GridLines="Both" AllowPaging="false" PageSize="1" ShowFooter="False" AllowSorting="True" ShowHeader="True" UseAccessibleHeader="True" RowStyle-Wrap="False" SelectedRowStyle-Wrap="False" TabIndex="1" HeaderStyle-Wrap="False" HeaderStyle-CssClass="CategoryBox" ShowHeaderWhenEmpty="True" EmptyDataText="<b>No data available. Please try later.</b>" >
                <Columns>
                    <asp:TemplateField HeaderText="&nbsp;<i class='fa-solid fa-folder-open'></i>&nbsp;FORUM CATEGORIES&nbsp;&nbsp;" ShowHeader="true"  HeaderStyle-Wrap="False" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#660066" HeaderStyle-ForeColor="#FFFFCC" ControlStyle-Font-Size="X-Large" ControlStyle-Font-Names="Verdana,Arial" ControlStyle-Font-Bold="True">
                        <ItemTemplate>
                        &nbsp;<i class="fa fa-list-alt" aria-hidden="true"></i>&nbsp;<a href="/Forums.aspx?CatID=<%# Eval("CAT_ID") %>"><b><%# Eval("CAT_NAME") %></b></a>&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns> 
        
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="False" Font-Size="Large" Font-Names="Verdana" Font-Bold="True" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <AlternatingRowStyle BackColor="#FFCCFF"  Wrap="False" Font-Size="Large" Font-Names="Verdana" Font-Bold="True" ForeColor="Black" />
            </asp:GridView>
        </div>

        <div class="col-md-8">

            <div class="alert alert-danger" role="alert">
                <h2 class="alert-heading"><i class="fa fa-bullhorn" aria-hidden="true"></i> System Announcement</h2>
                <hr />
                <p><strong>We need your help:</strong> This website is now under construction. It is not yet fully operational. If you need assistance, please call us at: <strong>1.713-714-7852</strong> or write to: <a href="mailto:support@aspnetportal.xyz"><strong>support@aspnetportal.xyz</strong></a></p>
    <p>If you wish to support this project, make your payments on line via PayPal at: <a target="_blank" href="http://paypal.me/inaaeb">http://paypal.me/inaaeb</a><br />
     Or send your donation via ZELLE at: <strong>713-493-5665</strong><br />
     Thank you in advance for your help and support!<br />
</p>
              <%' <hr><p class="mb-0">Whenever you need to, be sure to use margin utilities to keep things nice and tidy.</p> %>
            </div>

            <div class="alert alert-primary">
                <h2><i class="fa-solid fa-newspaper"></i> Faster, Smarter, Simpler, and Better</h2>
                <hr />
                <p class="lead"><strong>AspNetPortal.XYZ</strong> uses <strong>AspNetPortal</strong>. An ASP.NET (VB.NET/MVC) website portal application with a built-in firewall, an IP/USER-AGENT banning/unbanning control. A web bot tracking and security module. A user identity and authentication system. Several user account validation options. Passwords change and recovery. Internal human control (captcha) proteccion. Visitors log, ... and more.</p>
                <p class="lead"><strong>AspNetPortal</strong> provides you with a complete <strong>Content Management System, (CMS)</strong> so you can create blogs or import your Wordpress content, publish news and press releases, announce products and/or services, and post all types of classified ads or bulletins.</p>
                <p class="lead"><strong>AspNetPortal</strong> is packaged and distributed with a built-in <strong>Bulletin Board System, (BBS)</strong> to accomodate individuals and corporations searching for the best website portal application ever!.</p>
                <p><a runat="server" href="~/FAQ" class="btn btn-primary btn-lg">Learn more &raquo;</a> <a runat="server" target="_blank" href="http://mybbs.org/en/forum.asp?FORUM_ID=1790" class="btn btn-secondary btn-lg">Visit our forum &raquo;</a></p>
            </div>

        </div> 

    </div>

    <br />

    <div class="row">

        <div class="col-md-4">
             <div class="boxit">
                <div class="boxitheader">
                    <h2>Getting Started</h2>
                </div>
                <div class="boxittext">
                    <p>
                        Build dynamic web sites using an event-driven model. Create superb websites with familiar controls and components. Be as sophisticated as you can be building powerful and beatifull information systems ...
                    </p>
                    <p>
                        <a  runat="server" class="greenbtn btn-default" href="~/Help">Learn more &raquo;</a>
                    </p>
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <div class="boxit">
                <div class="boxitheader">
                    <h2>Decorate w/photos</h2>
                </div>
                <div class="boxittext">
                    <p>
                        Definitely, pictures are better than no picture at all! People prefer images because one alone saves a thousand words. Look at it on your smartphone. Ensure your main subject is clear. That’s key because your audience could be among the over 1 billion people using Facebook on mobile devices ...
                    </p>
                    <p>
                        <a runat="server" class="greenbtn btn-default" href="~/Help">Learn more &raquo;</a>
                    </p>
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <div class="boxit">
                <div class="boxitheader">
                    <h2>Web Hosting</h2>
                </div>
                <div class="boxittext">
                    <p>
                        You can easily find a web hosting company that offers you the right mix of hosting plans, features, and prices, for your Windows and Linux based applications. For a personalized service, try our own Web Hosting Service (MyWebHosting.XYZ) ...
                    </p>
                    <p>
                        <a class="greenbtn btn-default" target="_blank" href="http://mywebhosting.xyz">Learn more &raquo;</a>
                    </p>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
