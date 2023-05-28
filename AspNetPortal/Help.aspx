<%@ Page Title="Help" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Help.aspx.vb" Inherits="AspNetPortal._Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <hr />
    <table border="0" width="100%" cellspacing="0" cellpadding="0" align="center">
        <tr>
          <td bgcolor="black">
            <table border="0" width="100%" cellspacing="1" cellpadding="20">
              <tr>
                <td bgcolor="black"><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Help for Registered and Logged in Users Only</b></font></td>
              </tr>
              <tr>
                <td bgcolor="whitesmoke">

                <p><font face="Verdana, Arial, Helvetica" size="3" color="black">You can only perform any of the following options is you are successfully registered, verified and logged in to operate this system.</font></p>

                <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
               
                <ul>
                <li><span class="spnMessageText"><a href="#postad">How do I post a topic?</a></span></li>
                <li><span class="spnMessageText"><a href="#postbulletin">How do I post a bulletin?</a></span></li>
                <li><span class="spnMessageText"><a href="#postnews">How do I post a press release or news?</a></span></li>
                <li><span class="spnMessageText"><a href="#postad">How do I post any news?</a></span></li>
                <li><span class="spnMessageText"><a href="#register">Do I have to register?</a></span></li>
                <li><span class="spnMessageText"><a href="#smilies">How can I use smilies and images?</a></span></li>
                <li><span class="spnMessageText"><a href="#hyperlink">Can I add a hyperlink to my topics and messages?</a></span></li>
                <li><span class="spnMessageText"><a href="#format">Can I change the format of my text?</a></span></li>
                <li><span class="spnMessageText"><a href="#mods">What are moderators?</a></span></li>
                <li><span class="spnMessageText"><a href="#cookies">Are cookies used in this web site?</a></span></li>
                <li><span class="spnMessageText"><a href="#activetopics">What are active topics?</a></span></li>
                <li><span class="spnMessageText"><a href="#edit">Can I edit my own topics?</a></span></li>
                <li><span class="spnMessageText"><a href="#attach">Can I attach files to my own topics?</a></span></li>
                <li><span class="spnMessageText"><a href="#search">Can I search for any topics?</a></span></li>
                <li><span class="spnMessageText"><a href="#editprofile">Can I edit my profile?</a></span></li>
                <li><span class="spnMessageText"><a href="#signature">Can I attach my own signature to my posts?</a></span></li>
                <li><span class="spnMessageText"><a href="#announce">What are announcements?</a></span></li>
                <li><span class="spnMessageText"><a href="#censor">Are there any censor features?</a></span></li>
                <li><span class="spnMessageText"><a href="#pw">What do I do if I forget my password?</a></span></li>
                <li><span class="spnMessageText"><a href="#notify">Can I be notified by e-mail when there are new topics?</a></span></li>
                <li><span class="spnMessageText"><a href="#moderation">What does it mean if a region has moderation enabled?</a></span></li>
                <li><span class="spnMessageText"><a href="#COPPA">What is COPPA?</a></span></li>
                <li><span class="spnMessageText"><a href="mailto:support@MyBBS.org">Can't find your answer here? Send us an e-mail.</a></span></li>
                </ul>
                </font></p>
                </td>
              </tr>
              <tr>
                <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="popups"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Can't read any topics?</b></font></td>
              </tr>
              <tr>
                <td bgcolor="whitesmoke">
                    <p><font face="Verdana, Arial, Helvetica" size="3" color="black">This system displays all topics using <b>popup windows</b>. Some Internet browers intentionally <b>block popup windows</b> to prevent some websites bombing users with numerous and excesive advertising via popup windows. This is a nice feature available in almost all Internet browsers.</font></p>
                    <p><font face="Verdana, Arial, Helvetica" size="3" color="black">But we do not practise that form of non-sense advertising in this application. We only display topics that you specifically select and want to read. So, ... in this case, it is safe and required to allow popup windows displayed "specifically" from our website.</b></font></p>
                    <p><font face="Verdana, Arial, Helvetica" size="3" color="black">On <b>Firefox</b>, go to "<b>Settings</b>", click on "<b>Privacy and Security</b>", go to "<b>Permissions</b>", and find the "<b>Block and Popup Windows</b>" option. Make sure the checkbox is marqued and click on <b>Exceptions</b>. There you can add this website's URL (ex: <b>http://AspNetPortal.XYZ</b> ) to the list of allowed sites than can safely send you popup windows. After that, you should be able to read any ad you select in our website.</font></p>
                </td>
              </tr>

              <tr>
                <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="register"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Registering</b></font></td>
              </tr>
              <tr>
                <td bgcolor="whitesmoke">
                <p><font face="Verdana, Arial, Helvetica" size="3" color="black">Depending on system settings, registration may not be required to view current topics; however, if you wish to post a new ad or wish to reply to an existing ad, then registration is required. Registration is FREE. It only takes a few seconds. The only required fields are your email address and a login password. Other fields may be necessary in the future in order to group users with common interests.<br /><br />The information you provide during registration is not outsourced or used for any advertising other than displaying your topics. If you believe someone is sending you advertisements as a result of the information you provided through your registration process, please notify us immediately.</font></p></td>
              </tr>
              <tr>
                <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="smilies"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Smilies</b></font></td>
              </tr>
              <tr>
                <td bgcolor="whitesmoke">
                <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                You've probably seen others use smilies in their e-mail messages and/or other bulletin 
                board posts. Smilies are keyboard characters used to convey an emotion, such as a smile 
                <img src="/images/icon_smile.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /> or a frown 
                <img src="/images/icon_smile_sad.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" />. This web site 
                will automatically converts certain text to a graphical representation when it is 
                inserted between brackets [].&nbsp; Here are the smilies that are currently 
                supported by AspNetPortal:<br />
                  <table border="0" align="center" cellpadding="5">
                    <tr valign="top">
                      <td>
                        <table border="0" align="center">
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">smile</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:)]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_big.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">big smile</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:D]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_cool.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">cool</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[8D]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_blush.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">blush</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:I]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_tongue.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">tongue</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:p]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_evil.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">evil</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[}:)]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_wink.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">wink</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[;)]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_clown.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">clown</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:o)]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_blackeye.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">black eye</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[B)]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_8ball.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">eightball</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[8]</font></td>
                          </tr>
                        </table>
                      </td>
                      <td>
                        <table border="0" align="center">
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_sad.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">frown</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:(]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_shy.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">shy</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[8)]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_shock.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">shocked</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:0]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_angry.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">angry</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:(!]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_dead.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">dead</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[xx(]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_sleepy.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">sleepy</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[|)]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_kisses.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">kisses</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[:X]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_approve.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">approve</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[^]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_disapprove.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">disapprove</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[V]</font></td>
                          </tr>
                          <tr>
                            <td bgcolor="whitesmoke"><img src="/images/icon_smile_question.gif" width="20" height="20" border="0" alt="" title="" hspace="10" align="absmiddle" /></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">question</font></td>
                            <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">[?]</font></td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table></font></p>
                </td>
              </tr>
              <tr>
                <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="hyperlink"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Creating a Hyperlink in your topics and messages</b></font></td>
              </tr>
              <tr>
                <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">
                <p>You can easily add hyperlinka to your topics and/or messages.</p>
                <p>All you need to do is type the URL (http://MyBBS.org/en/), and it will automatically be converted to a URL (<span class="spnMessageText"><a href="http://MyBBS.org/en/" target="_blank">http://MyBBS.org/en/</a></span>)!</p>
                <p>The trick here is to type the prefix of your URL with <b>http://</b> or <b>https://</b> or <b>file://</b></p>
                <p>You can also add a mailto link to your message by typing in your e-mail address.<br />
                <blockquote>
                       <i>This Example:</i><br />
                       <b>support@MyBBS.org</b><br />
                       <i>Outputs this:</i><br />
                       <span class="spnMessageText"><a href="mailto:support@MyBBS.org">support@MyBBS.org</a></span></p>
                </blockquote><br />
                <p>Another way to add hyperlinks is to use the <b>[url]</b>linkto<b>[/url]</b> tags</p>
                <blockquote>
                       <i>This Example:</i><br />
                       <b>[url]</b>http://MyBBS.org/en/<b>[/url]</b> takes you home!<br />
                       <i>Outputs This:</i><br />
                       <span class="spnMessageText"><a href="http://MyBBS.org/en/">http://MyBBS.org/en/</a></span> takes you home!
                </blockquote></p>
                <p>If you use this tag: <b>[url=&quot;</b>linkto<b>&quot;]</b>description<b>[/url]</b> you can add a description to the link.</p>
                <blockquote>
                       <i>This Example:</i><br />
                       Take me to <b>[url=&quot;http://MyBBS.org/en/&quot;]</b>MyBBS Forums (EN)<b>[/url]</b><br />
                       <i>Outputs This:</i><br />
                       Take me to <span class="spnMessageText"><a href="http://MyBBS.org/en/">MyBBS Forums (EN)</a></span>
                </blockquote>
                <blockquote>
                       <i>This Example:</i><br />
                       If you have a question <b>[url=&quot;support@MyBBS.org&quot;]</b>E-Mail Me<b>[/url]</b><br />
                       <i>Outputs This:</i><br />
                       If you have a question <span class="spnMessageText"><a href="mailto:support@MyBBS.org">E-Mail Me</a></span>
                </blockquote>
                </p>
                </font></td>
              </tr>
              <tr>
                <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="format"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>How to format text with Bold, Italic, Quote, etc...</b></font></td>
              </tr>
              <tr>
                <td bgcolor="whitesmoke">
                <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                There are several Forum Codes you may use to change the appearance 
                of your text.&nbsp; Following is the list of codes currently available:</p>
                <blockquote>
                <p><b>Bold:</b> Enclose your text with [b] and [/b] .&nbsp; <i>Example:</i> This is <b>[b]</b>bold<b>[/b]</b> text. = This is <b>bold</b> text.</p>
                <p><i>Italic:</i> Enclose your text with [i] and [/i] .&nbsp; <i>Example:</i> This is <b>[i]</b>italic<b>[/i]</b> text. = This is <i>italic</i> text.</p>
                <p><u>Underline:</u> Enclose your text with [u] and [/u]. <i>Example:</i> This is <b>[u]</b>underline<b>[/u]</b> text. =  This is <u>underline</u> text.</p>
                <p><b>Aligning Text Left:</b> Enclose your text with [left] and [/left]
                </p>
                <p><b>Aligning Text Center:</b> Enclose your text with [center] and [/center]
                </p>
                <p><b>Aligning Text Right:</b> Enclose your text with [right] and [/right]
                </p>
                <p><b>Striking Text:</b> Enclose your text with [s] and [/s]<br />
                Example: <b>[s]</b>mistake<b>[/s]</b> = <s>mistake</s>
                </p>
                <p><b>Horizontal Rule:</b> Place a horizontal line in your post with [hr]<br />
                Example: <b>[hr]</b> = <hr noshade size="1">
                <p><b>Font Colors:</b><br />
                Enclose your text with [<i>fontcolor</i>] and [/<i>fontcolor</i>] <br />
                <i>Example:</i> <b>[red]</b>Text<b>[/red]</b> = <font color="red">Text</font id="red"><br />
                <i>Example:</i> <b>[blue]</b>Text<b>[/blue]</b> = <font color="blue">Text</font id="blue"><br />
                <i>Example:</i> <b>[pink]</b>Text<b>[/pink]</b> = <font color="pink">Text</font id="pink"><br />
                <i>Example:</i> <b>[brown]</b>Text<b>[/brown]</b> = <font color="brown">Text</font id="brown"><br />
                <i>Example:</i> <b>[black]</b>Text<b>[/black]</b> = <font color="black">Text</font id="black"><br />
                <i>Example:</i> <b>[orange]</b>Text<b>[/orange]</b> = <font color="orange">Text</font id="orange"><br />
                <i>Example:</i> <b>[violet]</b>Text<b>[/violet]</b> = <font color="violet">Text</font id="violet"><br />
                <i>Example:</i> <b>[yellow]</b>Text<b>[/yellow]</b> = <font color="yellow">Text</font id="yellow"><br />
                <i>Example:</i> <b>[green]</b>Text<b>[/green]</b> = <font color="green">Text</font id="green"><br />
                <i>Example:</i> <b>[gold]</b>Text<b>[/gold]</b> = <font color="gold">Text</font id="gold"><br />
                <i>Example:</i> <b>[white]</b>Text<b>[/white]</b> = <font color="white">Text</font id="white"><br />
                <i>Example:</i> <b>[purple]</b>Text<b>[/purple]</b> = <font color="purple">Text</font id="purple">
                </p>
                <p>&nbsp; <b>Headings:</b> Enclose your text with [h<i>number</i>] and [/h<i>n</i>]<br />
                  <table border="0">
                    <tr>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <i>Example:</i> <b>[h1]</b>Text<b>[/h1]</b> =
                      </font></td>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <h1>Text</h1>
                      </font></td>
                    </tr>
                    <tr>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <i>Example:</i> <b>[h2]</b>Text<b>[/h2]</b> =
                      </font></td>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <h2>Text</h2>
                      </font></td>
                    </tr>
                    <tr>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <i>Example:</i> <b>[h3]</b>Text<b>[/h3]</b> =
                      </font></td>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <h3>Text</h3>
                      </font></td>
                    </tr>
                    <tr>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <i>Example:</i> <b>[h4]</b>Text<b>[/h4]</b> =
                      </font></td>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <h4>Text</h4>
                      </font></td>
                    </tr>
                    <tr>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <i>Example:</i> <b>[h5]</b>Text<b>[/h5]</b> =
                      </font></td>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <h5>Text</h5>
                      </font></td>
                    </tr>
                    <tr>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <i>Example:</i> <b>[h6]</b>Text<b>[/h6]</b> =
                      </font></td>
                      <td><font face="Verdana, Arial, Helvetica" size="3" color="black">
                      <h6>Text</h6>
                      </font></td>
                    </tr>
                  </table>
                    </p>
                <p>&nbsp; </p>
                <p><b>Font Sizes:</b><br />
                <i>Example:</i> <b>[size=1]</b>Text<b>[/size=1]</b> = <font size="1">Text</font id="size1"><br />
                <i>Example:</i> <b>[size=2]</b>Text<b>[/size=2]</b> = <font size="2">Text</font id="size2"><br />
                <i>Example:</i> <b>[size=3]</b>Text<b>[/size=3]</b> = <font size="3">Text</font id="size3"><br />
                <i>Example:</i> <b>[size=4]</b>Text<b>[/size=4]</b> = <font size="4">Text</font id="size4"><br />
                <i>Example:</i> <b>[size=5]</b>Text<b>[/size=5]</b> = <font size="5">Text</font id="size5"><br />
                <i>Example:</i> <b>[size=6]</b>Text<b>[/size=6]</b> = <font size="6">Text</font id="size6">
                </p>
                <p>&nbsp; </p>
                <p><b>Bulleted List:</b> <b>[list]</b> and <b>[/list]</b>, and items in list with <b>[*]</b> and <b>[/*]</b>.</p>
                <p><b>Ordered Alpha List:</b> <b>[list=a]</b> and <b>[/list=a]</b>, and items in list with <b>[*]</b> and <b>[/*]</b>.</p>
                <p><b>Ordered Number List:</b> <b>[list=1]</b> and <b>[/list=1]</b>, and items in list with <b>[*]</b> and <b>[/*]</b>.</p>
                <p><b>Code:</b> Enclose your text with <b>[code]</b> and <b>[/code]</b>.</p>
                <p><b>Quote:</b> Enclose your text with <b>[quote]</b> and <b>[/quote]</b>.</p>
                </blockquote></font></td>
              </tr>
              <tr>
                <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="mods"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Moderators</b></font></td>
              </tr>
              <tr>
                <td bgcolor="whitesmoke">
                <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                Moderators control individual areas (Countries, States/Provinces or Cities). They may edit, delete, or prune any post in their areas. If you have a question about a particular area, you should direct it to your area moderator.</font></p></td>
              </tr>
              <tr>
                <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="cookies"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Cookies</b></font></td>
              </tr>
              <tr>
                <td bgcolor="whitesmoke">
                  <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                  These 
                      web site uses cookies to store some information like: the last time you logged in, your login name and       your encrypted password, your preferences, etc.  These cookies are stored on your hard drive. Cookies are not used to track your movement or perform any function other than to enhance your use of our web site. If you have not enabled cookies in your browser, many of these time-saving features will not work properly. <b>Also, you need to have cookies enabled if you want to enter an topics or reply to an existing ad.</b></p>
                  <p>You may delete all cookies set by this web site by selecting the red &quot;logoff&quot; button at the top of any page.
                  </font></p></td>
                </tr>
                <tr>
                  <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="activetopics"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Active topics</b></font></td>
                </tr>
                <tr>
                  <td bgcolor="whitesmoke">
                  <font face="Verdana, Arial, Helvetica" size="3" color="black">
                  <p>Active topics are tracked by cookies. When you click on the &quot;active topics&quot; link, a page is generated listing all topics that have been posted since your last visit to this web site (or approximately 20 minutes ago).</p>
                  </font></td>
                </tr>
                <tr>
                  <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="edit"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Editing your topics</b></font></td>
                </tr>
                <tr>
                  <td bgcolor="whitesmoke">
                  <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                  You may edit or delete your own posts at any time. Just go to the ad where the post to be edited or deleted is located and you will see an edit or delete icon (<img src="/images/icon_edit_ad.gif" width="20" height="20" border="0" alt="Edit" title="Edit" align="absmiddle" hspace="6" /><img src="/images/icon_delete_reply.gif" width="20" height="20" border="0" alt="Delete" title="Delete" align="absmiddle" hspace="6" />) on the line that begins &quot;posted on...&quot; Click on this icon to edit or delete the post. No one else can edit your post, except for the forum Moderator or the forum Administrator. </font></p></td>
                </tr>
                <tr>
                  <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="attach"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Attaching Files</b></font></td>
                </tr>
                <tr>
                  <td bgcolor="whitesmoke">
                  <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                  For security reasons, you may not attach files to any topics nor any messages. However, you may cut and paste text into your own topics and messages.</font></p></td>
               </tr>
               <tr>
                 <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="search"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Searching For Specific topics</b></font></td>
               </tr>
               <tr>
                 <td bgcolor="whitesmoke">
                 <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                 You may search for specific topics based on a word or words found in the ad, a user name, a date, and particular regions. Simply click on the &quot;search&quot; link at the top of most pages.</font></p></td>
               </tr>
               <tr>
                 <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="editprofile"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Editing your Profile</b></font></td>
               </tr>
               <tr>
                 <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">
                 <p>You may easily change any information stored in your profile by using the "profile" link located at the top of each page. Simply identify yourself by typing your e-mail address and your password and your profile information will appear on your screen. You may edit any information (except your e-mail address).</p></font></td>
               </tr>
               <tr>
                 <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="signature"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Signatures</b></font></td>
               </tr>
               <tr>
                 <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">
                 <p>You may attach signatures to the end of your posts when you post either a new ad or a reply. Your signature is editable on your &quot;profile&quot; page..</p>
                 <p>NOTE: HTML can't be used in signatures.</p></font></td>
               </tr>
               <tr>
                 <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="censor"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Censoring topics</b></font></td>
               </tr>
               <tr>
                 <td bgcolor="whitesmoke">
                 <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                     We censor certain words that may be posted; however, this censoring is not an exact science, and is being done based on the words that are being screened, so certain words may be censored out of context. By default, words that are censored are replaced with asterisks.</font></p></td>
               </tr>
               <tr>
                 <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="pw"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Lost Password</b></font></td>
               </tr>
               <tr>
                 <td bgcolor="whitesmoke">
                 <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                 Changing a password is simple! Assuming that e-mail features are turned on for this web site. The login page that requires you to identify yourself with your e-mail address and your password, also carry a &quot;lost or forgot password&quot; link that you can use to have a code e-mailed instantly to your e-mail address. That will allow you to create a new password again.</font></p>
                     <p><font face="Verdana, Arial, Helvetica" size="3" color="black">&nbsp;Because of the encryption method that we use for your password, we cannot tell you what your password is. So, the only solution is to change or re-enter your password.</font></p></td>
               </tr>
               <tr>
                 <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="notify"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>Can I be notified by e-mail when there are new topics?</b></font></td>
               </tr>
               <tr>
                 <td bgcolor="whitesmoke">
                 <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                 Yes, the <b>subscription</b> service allows you to subscribe to the entire site, or you can subscribe to individual regions (Country,State,City), categories, and/or specific topics/bulletins or news. You will receive an e-mail notifying you of new activity that has been posted to the area you have subscribed to. There are four levels of subscription services available in this web site:<br />
                 <blockquote>
                        - <b>Site Wide Subscription</b><br />
                        If you can subscribe to the entire 
                        web site, you will get a notification for any new posts made within all the regions and catgories in this web site.<br /><br />
                        - <b>Region Wide Subscription</b><br />
                        You can subscribe to a single Country, State or City. This will notify you of any new activity within the region that you subscribe.<br /><br />
                        - <b>Category Wide Subscription</b><br />
                        You can subscribe to an entire Category which will notify you if there was any 
                        new activity within any Category you choose.<br /><br />
                        - <b>Ad Wide Subscription</b><br />
                        More conveniently, you can subscribe to just any individual topics/bulletins/news. You will be notified of any new activity within that specific item.
                 </blockquote>
                 Each level of subscription is optional. The administrator can turn <b>On/Off</b> each level of subscription.<br />
                     <br />
                 To <b>Subscribe</b> or <b>Unsubscribe</b> from any level of subscription, you can use the "<b>Subscriptions</b>" link located near the top of your <b>Account</b> page to manage all your subscriptions. Or you can click on the subscribe/unsubscribe icon (<img src="/images/icon_subscribe.gif" width="20" height="20" border="0" alt="Subscribe" title="Subscribe" align="absmiddle" />&nbsp;<img src="/images/icon_unsubscribe.gif" width="20" height="20" border="0" alt="UnSubscribe" title="UnSubscribe" align="absmiddle" />) if you want to subscribe/unsubscribe to/from any area.</font></p></td>
               </tr>
               <tr>
                 <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="moderation"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>What happens during ad/reply moderation?</b></font></td>
               </tr>
               <tr>
                 <td bgcolor="whitesmoke">
                     <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                         <b>Moderation:</b> This feature allows the Administrator or the Moderator to "<b>Approve</b>", "<b>Hold</b>", &quot;<strong>Edit</strong>&quot; or "<b>Delete</b>" a user&#39;s Ad before it is shown to the public.</font></p>
                     <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                         <b>Approve:</b> Only the administrators or the moderators will be able to approve an ad. When the ad is approved, it will be made viewable to the public.</font></p>
                     <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                         <b>Hold:</b> When a user posts an ad or a message, the message is automatically put on hold until a moderator or an administrator approves it. No one will be able to view the post while it is put on hold.</font></p>
                     <p><font face="Verdana, Arial, Helvetica" size="3" color="black">
                         <i>NOTE: Authors of topics or replies will be able to edit their post during this mode.</i></font></p>
                     <p><font face="Verdana, Arial, Helvetica" size="3" color="black"><b>Edit:</b> If the administrator or moderator chooses this option, the post will be edited before approval and an e-mail will be sent to the poster of the ad/message informing them that their post was edited and approved. The administrator/moderator will be able to give their reason for edditing febore approving your post in the e-mail.<br /></font></p>
                     <p><font face="Verdana, Arial, Helvetica" size="3" color="black"><b>Delete:</b> If the administrator or moderator chooses this option, the post will be deleted and an e-mail will be sent to the poster of the ad/message informing them that their post was not approved. The administrator/moderator will be able to give their reason for not approving the post in the e-mail.<br /></font></p>
                 </td>
               </tr>
               <tr>
                 <td bgcolor="black"><a href="#top"><img src="/images/icon_go_up.gif" width="30" height="30" border="0" alt="Go To Top Of Page" title="Go To Top Of Page" align="right" /></a><a name="COPPA"></a><font face="Verdana, Arial, Helvetica" size="3" color="mintcream"><b>What is COPPA?</b></font></td>
               </tr>
               <tr>
                 <td bgcolor="whitesmoke"><font face="Verdana, Arial, Helvetica" size="3" color="black">
                 <p>The Children's Online Privacy Protection Act and Rule apply to individually identifiable information about a child that is collected online, such as full name, home address, e-mail address, telephone number or any other information that would allow someone to identify or contact the child. The Act and Rule also cover other types of information -- for example, hobbies, interests and information collected through cookies or other types of tracking mechanisms -- when they are tied to individually identifiable information. More information can be found <span class="spnMessageText"><a href="http://www.ftc.gov/bcp/conline/pubs/buspubs/coppa.htm" title="What is COPPA?">here</a></span>.</p></font></td>
            </font></td>
               </tr>
             </table>
           </td>
         </tr>
       </table>

</asp:Content>
