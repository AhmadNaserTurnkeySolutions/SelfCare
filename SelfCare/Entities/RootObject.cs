using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;

namespace SelfCare.Entities
{
//http://ahmadnaser.com/?json=get_recent_posts
//http://ahmadnaser.com/?json=get_category_index
//http://jsoneditoronline.org/
//http://json2csharp.com/




    public class RootObject
{
    public string status { get; set; }
    public int count { get; set; }
    public int count_total { get; set; }
    public int pages { get; set; }
    public List<Post> posts { get; set; }
}



  public class Category
{
    public int id { get; set; }
    public string slug { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public int parent { get; set; }
    public int post_count { get; set; }
   
    public ObservableCollection<Category> MyCategories { set; get; }
    public ObservableCollection<Post> Posts { set; get; }
    public Category()
    {
        MyCategories = new ObservableCollection<Category>();
        Posts = new ObservableCollection<Post>();
    }

    public void LoadPostsPerCategory()
    {
        var uri = new Uri("http://ahmadnaser.com/?json=get_recent_posts&count=10&page=1&cat="+this.id);
        var request = (HttpWebRequest)HttpWebRequest.Create(uri);


        //  request.BeginGetResponse(HandleCategoryResponse, request);


        IAsyncResult result = request.BeginGetResponse(HandleCategoryPerPost, request);
    }

    public void LoadCategories()
    {
        var uri = new Uri("http://ahmadnaser.com/?json=get_category_index");
        var request = (HttpWebRequest)HttpWebRequest.Create(uri);


        //  request.BeginGetResponse(HandleCategoryResponse, request);


        IAsyncResult result = request.BeginGetResponse(HandleCategoryResponse, request);

    }

    void HandleCategoryResponse(IAsyncResult ar)
    {

        var request = (HttpWebRequest)ar.AsyncState;




        //Normal Scenario 1 :: no getting objects
        /*
        using (var response = (HttpWebResponse)request.EndGetResponse(ar))
        using (var reader = new StreamReader(response.GetResponseStream()))
        {
            var json = reader.ReadToEnd();

        }
         * */


        try
        {


            //Fancy Scenario 2 ::  getting objects
            using (var response = (HttpWebResponse)request.EndGetResponse(ar))
            {



                var serializer = new DataContractJsonSerializer(typeof(CategoryRootObject));
                CategoryRootObject CategoryRootObject = (CategoryRootObject)serializer.ReadObject(response.GetResponseStream());
                //Dispatcher.BeginInvoke(() => DataContext = items.posts);
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    int i = 0;
                    foreach (var c in CategoryRootObject.categories)
                    {
                        MyCategories.Add(c);

                        i++;
                    }
                }
            );


            }
        }
        catch (Exception ex)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Success 1"));
        }

    }
          
    
    
               








    void HandleCategoryPerPost(IAsyncResult ar)
    {
        var request = (HttpWebRequest)ar.AsyncState;

         try
        {

        //Fancy Scenario 2 ::  getting objects
        using (var response = (HttpWebResponse)request.EndGetResponse(ar))
        {



            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            RootObject ps = (RootObject)serializer.ReadObject(response.GetResponseStream());
            //Dispatcher.BeginInvoke(() => DataContext = items.posts);
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                int i = 0;
                foreach (var c in ps.posts)
                {
                    if (!this.Posts.Contains(c))
                    this.Posts.Add(c);

                }
            }
        );
   



        }

        }
         catch (Exception ex)
         {

             Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Failed"));
         }
            
    }
               
        


   





}

  public class CategoryRootObject
  {
      public string status { get; set; }
      public int count { get; set; }
      public List<Category> categories { get; set; }
  }


public class Author
{
    public int id { get; set; }
    public string slug { get; set; }
    public string name { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string nickname { get; set; }
    public string url { get; set; }
    public string description { get; set; }
}

public class Full
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Thumbnail
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Medium
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Large
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionSliderRetina
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionSlider
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionBlog
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionPortfolioThumb
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionSingle
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionSingleUncropped
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Images
{
    public Full full { get; set; }
    public Thumbnail thumbnail { get; set; }
    public Medium medium { get; set; }
    public Large large { get; set; }
    //public ProgressionSliderRetina __invalid_name__progression-slider-retina { get; set; }
    //public ProgressionSlider __invalid_name__progression-slider { get; set; }
    //public ProgressionBlog __invalid_name__progression-blog { get; set; }
    //public ProgressionPortfolioThumb __invalid_name__progression-portfolio-thumb { get; set; }
    //public ProgressionSingle __invalid_name__progression-single { get; set; }
    //public ProgressionSingleUncropped __invalid_name__progression-single-uncropped { get; set; }
}

public class Attachment
{
    public int id { get; set; }
    public string url { get; set; }
    public string slug { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string caption { get; set; }
    public int parent { get; set; }
    public string mime_type { get; set; }
    public Images images { get; set; }
}

public class CustomFields
{
}

public class Full2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Thumbnail2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Medium2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class Large2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionSliderRetina2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionSlider2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionBlog2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionPortfolioThumb2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionSingle2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ProgressionSingleUncropped2
{
    public string url { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}

public class ThumbnailImages
{
    public Full2 full { get; set; }
    public Thumbnail2 thumbnail { get; set; }
    public Medium2 medium { get; set; }
    public Large2 large { get; set; }
    //public ProgressionSliderRetina2 __invalid_name__progression-slider-retina { get; set; }
    //public ProgressionSlider2 __invalid_name__progression-slider { get; set; }
    //public ProgressionBlog2 __invalid_name__progression-blog { get; set; }
    //public ProgressionPortfolioThumb2 __invalid_name__progression-portfolio-thumb { get; set; }
    //public ProgressionSingle2 __invalid_name__progression-single { get; set; }
    //public ProgressionSingleUncropped2 __invalid_name__progression-single-uncropped { get; set; }
}

public class Post : IEquatable<Post>
{
    public int id { get; set; }
    public string type { get; set; }
    public string slug { get; set; }
    public string url { get; set; }
    public string status { get; set; }
    public string title { get; set; }
    public string title_plain { get; set; }
    public string content { get; set; }
    public string excerpt { get; set; }
    public string date { get; set; }
    public string modified { get; set; }
    public List<Category> categories { get; set; }
    public List<object> tags { get; set; }
    public Author author { get; set; }
    public List<object> comments { get; set; }
    public List<Attachment> attachments { get; set; }
    public int comment_count { get; set; }
    public string comment_status { get; set; }
    public string thumbnail { get; set; }
    public CustomFields custom_fields { get; set; }
    public string thumbnail_size { get; set; }
    public ThumbnailImages thumbnail_images { get; set; }
    
    public bool Equals(Post other)
    {
        if (other == null)
            return false;

        return this.id == other.id;
    }
}

public class Data
{
    public string url { get; set; }
    public bool is_silhouette { get; set; }
}

public class Picture
{
    public Data data { get; set; }
}

public class FbRootObject
{
    public string gender { get; set; }
    public string link { get; set; }
    public string username { get; set; }
    public string id { get; set; }
    public Picture picture { get; set; }
}



public class CategoryMenu
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public bool IsDataLoaded { get; set; }

    public ObservableCollection<ItemsRootObject> Items { set; get; }

    public CategoryMenu() 
    {

        Items = new ObservableCollection<ItemsRootObject>();
    }

    public void LoadItems() 
    {
        WebClient webClient = new WebClient();
        webClient.DownloadStringCompleted += webClient2_DownloadStringCompleted;
     //   ProgressBarRequest.Visibility = System.Windows.Visibility.Visible;
        webClient.DownloadStringAsync(new Uri("http://localhost:2819/Category/GetAllItemsInCategory/"+this.Id));
    }

    void webClient2_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(e.Result))
            {
                //Parse JSON result as POCO 
                var root1 = JsonConvert.DeserializeObject<List<ItemsRootObject>>(e.Result);

                var x = root1.Count;

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                   
                    foreach (var item in root1)
                    {
                        this.Items.Add(item);
                       
                    }
                }
            );

                //  string info = string.Format("Id : {0}\nUserName : {1}\n{2}", root1.username, root1.id, root1.gender);


            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
        finally
        {
         //   ProgressBarRequest.Visibility = System.Windows.Visibility.Collapsed;
        }

    }

}

public class ItemsRootObject
{
    public CategoryMenu CategoryMenu { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int price { get; set; }
    public int CategoryMenuId { get; set; }
}






}
