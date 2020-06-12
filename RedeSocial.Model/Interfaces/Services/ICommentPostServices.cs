using System;
using System.Collections.Generic;
using System.Text;
using RedeSocial.Model.Entity;
using RedeSocial.Model.Interfaces.Repositories;

namespace RedeSocial.Model.Interfaces.Services
{
    public interface ICommentPostServices:IBaseServices<CommentPostModel>,ICommentPostRepository
    {
    }
}
