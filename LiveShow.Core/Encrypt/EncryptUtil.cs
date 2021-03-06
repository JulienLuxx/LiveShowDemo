﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LiveShow.Core.Encrypt
{
    public class EncryptUtil: IEncryptUtil
    {
        #region Md5

        public string GetMd5(string value, Encoding encoding, int? startIncdex, int? length)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            var md5 = new MD5CryptoServiceProvider();
            string result;
            try
            {
                var hash = md5.ComputeHash(encoding.GetBytes(value));
                result = startIncdex == null && length == null ? BitConverter.ToString(hash) : BitConverter.ToString(hash, startIncdex.Value, length.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                md5.Clear();
            }
            return result.Replace("-", "");
        }

        public string GetMd5By16(string value)
        {
            return GetMd5By16(value, Encoding.UTF8);
        }

        public string GetMd5By16(string value, Encoding encoding)
        {
            return GetMd5(value, encoding, 4, 8);
        }

        public string GetMd5By32(string value)
        {
            return GetMd5By32(value, Encoding.UTF8);
        }

        public string GetMd5By32(string value, Encoding encoding)
        {
            return GetMd5(value, encoding, null, null);
        }

        #endregion
    }
}
