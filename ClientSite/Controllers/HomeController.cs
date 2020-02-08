﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClientTelentCommucication;
using MessageParser;
using Microsoft.AspNetCore.Mvc;
using TelnetCommunication;
using static Shared.TagWrapper.TagWrapper;

namespace ClientSite.Controllers
{
    public class HomeController : Controller
    {
        public static ClientHandler clientHandler;

        public IActionResult Index()
        {
            if (clientHandler == null)
            {
                clientHandler = new ClientHandler("10.0.1.3", 52475, new JsonMudMessage());
            }

            return View();
        }

        public ActionResult SendCommand(string guid, string command)
        {
            if (command != string.Empty)
            {
                clientHandler.OutQueue.Enqueue(command);
            }

            List<ParsedMessage> parsedMessages = new List<ParsedMessage>();

            string message;
            while (clientHandler.InQueue.TryDequeue(out message))
            {
                if (message.StartsWith("<Sound>"))
                {
                    //handle sound
                }
                else if (message.StartsWith("<Map>"))
                {
                    //update map
                }
                else if (message.StartsWith("<Data>"))
                {
                    //not going to local cache stuff so n/a?
                }
                else if (message.StartsWith("<FileValidation>"))
                {
                    //not going to local cache stuff so n/a?
                }
                else
                {
                    parsedMessages.AddRange(Parser.Parse(message));
                }
            }

            List<Tuple<string, string>> tuples = ConvertParsedMessageToTuples(parsedMessages);

            return Json(tuples);
        }

        private List<Tuple<string, string>> ConvertParsedMessageToTuples(List<ParsedMessage> parsedMessages)
        {
            List<Tuple<string, string>> tuples = new List<Tuple<string, string>>();

            foreach (ParsedMessage parsedMessage in parsedMessages)
            {
                tuples.Add(new Tuple<string, string>(parsedMessage.TagType.ToString(), parsedMessage.Message));
            }

            return tuples;
        }
    }
}
