﻿using System;
using System.Threading.Tasks;
using RestSharp;

namespace PuppeteerExtraSharp6.Plugins.Recaptcha.RestClient
{
    public class PollingBuilder<T>
    {
        private readonly IRestClient _client;
        private readonly IRestRequest _request;
        private int _timeout = 5;
        private int _limit = 5;
        public PollingBuilder(IRestClient client, IRestRequest request)
        {
            _client = client;
            _request = request;
        }

        public PollingBuilder<T> WithTimeoutSeconds(int timeout)
        {
            _timeout = timeout;
            return this;
        }

        public PollingBuilder<T> TriesLimit(int limit)
        {
            _limit = limit;
            return this;
        }

        public async Task<IRestResponse<T>> ActivatePollingAsync(Func<IRestResponse<T>, PollingAction> resultDelegate)
        {
            var response = await _client.ExecuteAsync<T>(_request);

            if (resultDelegate(response) == PollingAction.Break || _limit <= 1)
                return response;

            await Task.Delay(_timeout * 1000);
            _limit -= 1;

            return await ActivatePollingAsync(resultDelegate);
        }
    }

    public enum PollingAction
    {
        ContinuePolling,
        Break
    }
}
