namespace Confetti
{
    public class Settings
    {
        private readonly IRawSettingsSource source;
        private readonly IRawSettingParser parser;

        public Settings(IRawSettingsSource source, IRawSettingParser parser)
        {
            this.source = source;
            this.parser = parser;
        }

        /// <summary>
        /// Gets the setting named `key`, parsed as a value of type `T`
        /// </summary>
        /// <typeparam name="T">The type of the value to return</typeparam>
        /// <param name="key">The name of the setting key</param>
        /// <returns>The setting named `key`, parsed as a value of type `T`</returns>
        public T GetSetting<T>(string key)
        {
            string rawValue;
            if (!source.TryGetRawSetting(key, out rawValue))
            {
                throw new MissingKeyException(key);
            }

            var result = parser.Parse<T>(rawValue);
            if (result.IsFailure)
            {
                throw new MalformedValueException();
            }

            return result.Value;
        }
    }
}
