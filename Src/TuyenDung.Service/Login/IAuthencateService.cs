﻿using TuyenDung.Common.XBaseModel;
using TuyenDung.Model;

namespace TuyenDung.Service
{
    public interface IAuthencateService
    {
        Task<ApiResult<string>> Authencate(LoginModel request);
    }
}