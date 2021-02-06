using System;
using System.Collections.Generic;
using System.Text;
using CollabAssist.Incoming;
using CollabAssist.Incoming.Models;
using CollabAssist.Output;

namespace CollabAssist.Services
{
    public class BuildService
    {
        private readonly IInputHandler _inputHandler;
        private readonly IOutputHandler _outputHandler;

        public BuildService(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
        }

        public bool HandleBuild(Build build)
        {
            return false;
        }
    }
}
