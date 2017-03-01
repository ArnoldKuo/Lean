﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/


using System;

namespace QuantConnect.VisualStudioPlugin
{
    /// <summary>
    /// A command to perform QuantConnect authentication
    /// </summary>
    class LogInCommand
    {
        /// <summary>
        /// Perform QuantConnect authentication
        /// </summary>
        /// <param name="serviceProvider">Visual Studio services provider</param>
        /// <returns>true if user logged into QuantConnect, false otherwise</returns>
        public static bool DoLogIn(IServiceProvider serviceProvider)
        {
            var authorizationManager = AuthorizationManager.GetInstance();
            if (authorizationManager.IsLoggedIn())
            {
                return true;
            }

            var logInDialog = new LogInDialog(authorizationManager);
            logInDialog.HasMinimizeButton = false;
            logInDialog.HasMaximizeButton = false;
            logInDialog.ShowModal();

            Credentials? credentials = logInDialog.GetCredentials();

            if (credentials.HasValue)
            {
                VsUtils.DisplayInStatusBar(serviceProvider, "Logged into QuantConnect");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}