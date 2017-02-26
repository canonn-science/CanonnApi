﻿using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace RuinsApi.Net.Http
{
	public class JsonContent : StringContent
	{
		public JsonContent(object obj)
			: this(JsonConvert.SerializeObject(obj)) { }

		public JsonContent(string jsonString)
			: base(jsonString, Encoding.UTF8, "application/json")
		{ }
	}
}
