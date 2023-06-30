using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LayoutBLL
    {
        CategoryDAO CategoryDAO = new CategoryDAO();
        SocialMediaDAO socialdao = new SocialMediaDAO();
        FavDAO favdao = new FavDAO();
        MetaDAO metadao = new MetaDAO();
        AddressDAO addressdao = new AddressDAO();
        PostDAO postdao = new PostDAO();
        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.Categories = CategoryDAO.GetCategory();
            List<SocialMediaDTO> socialmedialist = new List<SocialMediaDTO>();
            socialmedialist = socialdao.GetSocialMedia();
            dto.Facebook = socialmedialist.First(x => x.Link.Contains("facebook"));
            dto.Twitter = socialmedialist.First(x => x.Link.Contains("twitter"));
            dto.Instagram = socialmedialist.First(x => x.Link.Contains("instagram"));
            dto.YouTube = socialmedialist.First(x => x.Link.Contains("youtube"));
            dto.Linkdin = socialmedialist.First(x => x.Link.Contains("linkedin"));
            dto.FavDTO = favdao.GetFav();
            dto.Metalist = metadao.GetMetaData();
            List<AddressDTO> Addresslist = addressdao.GetAddress();
            dto.Address = Addresslist.First();
            dto.HotNews = postdao.GetHotNews();
            return dto;
        }
    }
}
