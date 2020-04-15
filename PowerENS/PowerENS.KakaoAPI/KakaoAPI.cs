using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PowerENS.KakaoAPI
{
    public class KakaoAPI
    {
       
        private string _serviceUrl { get; set; }
        private string _requestUrl { get; set; }
        private string _authToken { get; set; }
        private string _serverName { get; set; }
        private int _service { get; set; }
        private string _mobile { get; set; }

        public string SendMessage(string _message, int _template)
        {
            try
            {
                var client = new RestClient(_serviceUrl);
                var request = new RestRequest(_requestUrl, Method.POST);
                request.AddHeader("authToken", _authToken);
                request.AddHeader("serverName", _serverName);
                request.AddHeader("paymentType", "P");

                request.AddJsonBody(new { service = _service, mobile = _mobile, message = _message, template = _template });

                IRestResponse response = client.Execute(request);

                return response.Content;
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }

        }

    }
}
