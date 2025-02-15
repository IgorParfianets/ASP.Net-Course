﻿using MediatR;

namespace AspNetSample.Data.CQS.Commands;

public class AddRefreshTokenCommand : IRequest
{
    public Guid TokenValue;
    public Guid UserId;
}