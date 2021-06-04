﻿/*
 * Magic, Copyright(c) Thomas Hansen 2019 - 2021, thomas@servergardens.com, all rights reserved.
 * See the enclosed LICENSE file for details.
 */

using System.Linq;
using magic.node;
using magic.signals.contracts;

namespace magic.lambda.sockets
{
    /// <summary>
    /// [sockets.users] slot that returns currently connected users.
    /// </summary>
    [Slot(Name = "sockets.users")]
    public class Users : ISlot
    {
        /// <summary>
        /// Slot implementation.
        /// </summary>
        /// <param name="signaler">Signaler that raised signal.</param>
        /// <param name="input">Arguments to slot.</param>
        public void Signal(ISignaler signaler, Node input)
        {
            // House cleaning.
            input.Clear();
            input.Value = null;

            // Retrieving users and iterating through them.
            foreach (var idxUser in MagicHub.GetUsers())
            {
                // Creating a return node for currently iterated user and each connection user has.
                var cur = new Node(".");
                var username = new Node("username", idxUser.Username);
                cur.Add(username);
                var roles = new Node("connections");
                foreach (var idxRole in idxUser.Connections)
                {
                    roles.Add(new Node(".", idxRole));
                }
                cur.Add(roles);

                // Adding currently iterated user to return node.
                input.Add(cur);
            }
        }
    }
}
