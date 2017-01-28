﻿using Data.Domain;
using Desktop.BaseLib;
using Desktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Controllers
{
    class MainController
    {
        private IMainView _view;
        private Korisnik _user;
        private IView _parent;
        
        public MainController(IMainView view, Korisnik user, IView parent)
        {
            _view = view;
            _user = user;
            _parent = parent;
        }

        public void Logout()
        {
            _view.Close();
        }

        public void ShowDatabaseSets()
        {
            var newForm = new frmDatabaseParts(_user);
            newForm.ShowDialog();
        }

        public void ShowDatabaseParts()
        {
            //TODO showdatabaseparts
        }

        public void ShowInventorySets()
        {
            //TODO showinventorysets
        }

        public void ShowInventoryParts()
        {
            //TODO showinventoryparts
        }
    }
}
