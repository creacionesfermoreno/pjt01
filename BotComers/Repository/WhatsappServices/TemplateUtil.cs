using BotComers.Controllers;
using E_DataModel.Gimnasio;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotComers.Repository.WhatsappServices
{
    public class TemplateUtil
    {
        public object TemplateHeaderText(string val)
        {
            if (val.Contains("{{1}}"))
            {
                return new
                {
                    type = "HEADER",
                    format = "TEXT",
                    text = val,
                    example = new
                    {
                        header_text = generateExampleText(val),
                    }
                };
            }
            else
            {
                return new
                {
                    type = "HEADER",
                    format = "TEXT",
                    text = val,
                };

            }

        }
        public object TemplateBody(string val)
        {
            if (val.Contains("{{1}}"))
            {
                return new
                {
                    type = "BODY",
                    text = val,
                    example = new
                    {
                        body_text = new List<object> { generateExampleText(val) }
                    }
                };
            }
            else
            {
                return new
                {
                    type = "BODY",
                    text = val,
                };
            }
        }

        public object TemplateFooter(string val)
        {
            return new
            {
                type = "FOOTER",
                text = val,
            };
        }

        public List<string> generateExampleText(string txt)
        {
            char x = '{';
            int count = txt.Count(f => f == x);
            int total = count / 2;

            List<string> list = new List<string>();
            for (int i = 0; i < total; i++)
            {
                list.Add("example-text");
            }
            return list;

        }

        public object TemplateImage(string handle)
        {
            return new
            {
                type = "HEADER",
                format = "IMAGE",
                example= new {
                header_handle= new List<string>() { handle }
            }
        };
        }

        public object TemplateDoc(string handle)
        {
            return new
            {
                type = "HEADER",
                format = "DOCUMENT",
                example = new
                {
                    header_handle = new List<string>() { handle }
                }
            };
        }

        public object TemplateVideo(string handle)
        {
            return new
            {
                type = "HEADER",
                format = "VIDEO",
                example = new
                {
                    header_handle = new List<string>() { handle }
                }
            };
        }

        public object TemplateAction(ButtonsOptional buttons)
        {
            List<object> list = new List<object>();
            if (buttons.data?.phone_text != null)
            {
                list.Add(new
                {
                    type = "PHONE_NUMBER",
                    text = buttons.data?.phone_text,
                    phone_number = buttons.data?.phonevalue
                });
            }

            if (buttons.data?.url_text != null)
            {
                list.Add(new
                {
                    type = "URL",
                    text = buttons.data?.url_text,
                    url = buttons.data?.url_link,
                });
            }

            return new
            {
                type = "BUTTONS",
                buttons = list,
            };

        }

        public object TemplateQuickReply(ButtonsOptional buttons)
        {
            List<Button> list = new List<Button>();
            foreach (var item in buttons.valueQuick)
            {
                list.Add(new Button() { type = "QUICK_REPLY", text = item });
            }
            return new
            {
                type = "BUTTONS",
                buttons = list,
            };
        }

        //*********************************************** UTILS FOR SEND TEMPLATE PARAMS*****************************************


        public object ParmsHeader(string type, string value)
        {
            List<object> parms = new List<object>();

            object image = new
            {
                type = "image",
                image = new
                {
                    link = value,
                }
            };

            object document = new
            {
                type = "document",
                document = new
                {
                    link = value,
                    filename = "document.pdf",
                }
            };

            object video = new
            {
                type = "video",
                video = new
                {
                    link = value,
                }
            };

            switch (type)
            {
                case "TEXT":
                    parms.Add(TextParam(value));
                    break;
                case "IMAGE":
                    parms.Add(image);
                    break;
                case "DOCUMENT":
                    parms.Add(document);
                    break;
                case "VIDEO":
                    parms.Add(video);
                    break;
                default:
                    break;
            }


            return new
            {
                type = "header",
                parameters = parms,
            };
        }


        public object Parmsbody(List<string> values)
        {
            List<object> parms = new List<object>();

            foreach (var item in values)
            {
                parms.Add(TextParam(item));
            }

            return new
            {
                type = "body",
                parameters = parms,
            };
        }

        public object TextParam(string txt)
        {
            return new
            {
                type = "text",
                text = txt,
            };
        }

        public SendModelTemplate FormatSendTemplate(WhatsappConfigDTO campaign, string number, List<string> values)
        {

            SendModelTemplate _send = new SendModelTemplate();
            _send.messaging_product = "whatsapp";
            _send.recipient_type = "individual";
            _send.to = number;
            _send.type = "template";

            TemplateSend _template = new TemplateSend();
            _template.name = campaign?.NameTemplate;
            _template.language = new Language() { code = campaign.Languaje };

            List<object> componenst = new List<object>();

            if (campaign?.TypeHeader == "VIDEO")
            {
                componenst.Add(ParmsHeader("VIDEO", campaign?.ParametersHeader));
            }
            else if (campaign?.TypeHeader == "IMAGE")
            {
                componenst.Add(ParmsHeader("IMAGE", campaign?.ParametersHeader));
            }
            else if (campaign?.TypeHeader == "DOCUMENT")
            {
                componenst.Add(ParmsHeader("DOCUMENT", campaign?.ParametersHeader));
            }
            else if (campaign?.TypeHeader == "TEXT")
            {
                if (campaign?.ParametersHeader != null && campaign?.ParametersHeader != "")
                {
                    var val = values[int.Parse(campaign?.ParametersHeader)];
                    componenst.Add(ParmsHeader("TEXT", val));
                }

            }

            if (campaign?.ParametersBody != null && campaign?.ParametersBody != "")
            {
                string texto = campaign?.ParametersBody;
                var textoArray = texto.Split('|');

                List<string> bodyp = new List<string>();
                for (int i = 0; i < textoArray.Count() - 1; i++)
                {
                    var val = values[int.Parse(textoArray[i])];
                    bodyp.Add(val);
                }

                componenst.Add(Parmsbody(bodyp));
            }

            if (componenst.Count > 0)
            {
                _template.components = componenst;
            }

            _send.template = _template;
            return _send;
        }


        //*********************************************** UTILS FOR SEND TEMPLATE PARAMS*****************************************
        //*********************************************** UTILS GLOBALS *****************************************


        //*********************************************** UTILS GLOBALS *****************************************


    }
    public class Button
    {
        public string type { get; set; }
        public string text { get; set; }
    }


}