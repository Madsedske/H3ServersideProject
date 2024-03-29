﻿namespace H3ServersideProject.Repositories.Helpers
{

    /// <summary>
    /// The class for sessions.
    /// </summary>
    public class SessionVariables
    {
        public const string SessionKeyUsername = "SessionKeyUsername";
        public const string SessionKeySessionId = "SessionKeySessionId";
    }

    public enum SessionKeyEnum
    {
        SessionKeyUsername = 0,
        SessionKeySessionId = 1
    }
}