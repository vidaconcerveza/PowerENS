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
       

        public string SendMessage(string _authToken, string _serverName, int _service, string _mobile ,string _message, int _template)
        {
            try
            {
                var client = new RestClient("https://talkapi.lgcns.com");
                var request = new RestRequest("/request/kakao.json", Method.POST);

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
