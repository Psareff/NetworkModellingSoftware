using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace NetworkModellingSoftware
{
    internal class NodeInfo
    {
        string _name, _manufacter, _model, _type;
        int _inputDocksQty, _outputDocksQty, _bandwidth;
        bool _isSuccessfullyCreated = true;
        private NetworkNodeType _networkNodeType;

        public NodeInfo(NetworkNodeType networkNodeType, string name, string manufacter, string model, string inputDocksQty, string outputDocksQty, string bandwidth)
        {
            _networkNodeType = networkNodeType;


            try
            {
                _inputDocksQty = Int32.Parse(inputDocksQty);
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong input dock quantity entered {" + inputDocksQty + "}",
                        "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _isSuccessfullyCreated = false;
            }
            try
            {
                _outputDocksQty = Int32.Parse(outputDocksQty);
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong output dock quantity entered {" + outputDocksQty + "}",
                        "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _isSuccessfullyCreated = false;

            }
            switch (networkNodeType)
            {
                case NetworkNodeType.PROVIDER:
                    if (_inputDocksQty > 0 || _outputDocksQty != 1)
                    {
                        MessageBox.Show("Provider can't have input sockets and more than one output",
                            "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _isSuccessfullyCreated = false;
                    }
                    break;
                case NetworkNodeType.CLIENT:
                    if (_inputDocksQty !=1 || _outputDocksQty > 1)
                    {
                        MessageBox.Show("Client can't have more than one output socket and less than one input",
                        "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _isSuccessfullyCreated = false;

                    }

                    break;
                case NetworkNodeType.HUB:
                    if (_inputDocksQty != 1 || _outputDocksQty <= 2)
                    {
                        MessageBox.Show("Hub can't have more than one input socket and less than two output sockets",
                            "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _isSuccessfullyCreated = false;
                    }
                    break;
                case NetworkNodeType.BRIDGE:
                    if (_inputDocksQty != 1  || _outputDocksQty < 2)
                    {
                        MessageBox.Show("Bridge can't have less than one input socket and can have only one input socket",
                            "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _isSuccessfullyCreated = false;
                    }
                    break;
                case NetworkNodeType.ROUTER:
                    if (_inputDocksQty < 1 || _outputDocksQty < 1)
                    {
                        MessageBox.Show("Router can't have less than one input socket and less than one output socket",
                            "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _isSuccessfullyCreated = false;
                    }
                    break;
                case NetworkNodeType.SWITCH:
                    if (_inputDocksQty != 1 || _outputDocksQty < 2)
                    {
                        MessageBox.Show("Switch can't have more than one input socket and less than one output socket",
                            "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _isSuccessfullyCreated = false;
                    }
                    break;
                case NetworkNodeType.SERVER:
                    if (_inputDocksQty <= 0 || _outputDocksQty <= 0)
                    {
                        MessageBox.Show("Server can't have less than one input socket and less than one output socket",
                            "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        _isSuccessfullyCreated = false;
                    }
                    break;

            }
            _networkNodeType = networkNodeType;

            try
            {
                _bandwidth = Int32.Parse(bandwidth);
            }
            catch (FormatException)
            {
                MessageBox.Show("Wrong bandwidth entered {" + bandwidth + "}",
                        "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _isSuccessfullyCreated = false;
            }

            if (manufacter != null || manufacter.Length != 0)
                _manufacter = manufacter;
            else
            {
                MessageBox.Show("No manufacter name entered",
            "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _isSuccessfullyCreated=false;
            }
            if (name != null || name.Length != 0)
                _name = name;
            else
            {
                MessageBox.Show("No name entered",
                        "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _isSuccessfullyCreated = false;
            }
            if (model != null || model.Length != 0)
                _model = model;
            else
            {
            MessageBox.Show("No model name entered",
                        "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _isSuccessfullyCreated = false;
            }
            _type = _networkNodeType.ToString();
        }

        public string manufacter
        {
            get => _manufacter;
        }
        public string name
        {
            get => _name;
        }
        public string model
        {
            get => _model;
        }
        public string type
        {
            get => _type;
        }
        public int bandwidth
        {
            get => _bandwidth;
        }
        public int inputDocksQty
        {
            get => _inputDocksQty;
        }
        public int outputDocksQty
        {
            get => _outputDocksQty;
        }
        public bool isSuccessfullyCreated
        {
            get => _isSuccessfullyCreated;
        }

    }
}
