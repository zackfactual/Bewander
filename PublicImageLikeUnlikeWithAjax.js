//At PublicImage.cshtml when user is loggged in may like image user ajax to post to database, update total likes but do not refresh page
 function LikeImage(ImageID, element) {
    var likeIcon = document.getElementById(ImageID);
    var imageLiked = false;
    var totalLikeCount = document.getElementById(ImageID + "-LikeCount");
    var likePluralize = document.getElementById(ImageID + "-LikePluralize");

    $.ajax({
        type: "POST",
        url: "/Posts/LikeImage?ID=" + ImageID,
        
        success: function () {
            element.setAttribute("onclick", "UnlikeImage('" + ImageID + "', this)"); //changes like image to selected
            totalLikeCount.innerHTML = parseInt(totalLikeCount.innerHTML) + 1; //pulls total likes and adds one
            // reformat image like counter on like
            if (parseInt(totalLikeCount.innerHTML) == 1) {
                likePluralize.innerHTML = "<span> Like</span>";
            }
            if (parseInt(totalLikeCount.innerHTML) != 1) {
                likePluralize.innerHTML = "<span> Likes</span>";
            }
        }
    });

    imageLiked = true;
    if (imageLiked == true) {
        $(likeIcon).removeClass('like-post').addClass('unlike-post');
    }
}

//At PublicImage.cshtml when user is loggged in may like image user ajax to post to database, update total likes but do not refresh page
function UnlikeImage(ImageID, element) {
    var likeIcon = document.getElementById(ImageID);
    var imageLiked = true;
    var totalLikeCount = document.getElementById(ImageID + "-LikeCount");
    var likePluralize = document.getElementById(ImageID + "-LikePluralize");

    $.ajax({
        type: "POST",
        url: "/Posts/UnlikeImage?ID=" + ImageID,

        success: function () {
            element.setAttribute("onclick", "LikeImage('" + ImageID + "', this)");  //changes like image to deselected
            totalLikeCount.innerHTML = parseInt(totalLikeCount.innerHTML) - 1; //pulls total likes and subtracts one
            // reformat image like counter on unlike
            if (parseInt(totalLikeCount.innerHTML) == 1) {
                likePluralize.innerHTML = "<span> Like</span>";
            }
            if (parseInt(totalLikeCount.innerHTML) != 1) {
                likePluralize.innerHTML = "<span> Likes</span>";
            }
        }
    });

    imageLiked = false;
    if (imageLiked == false) {
        $(likeIcon).removeClass('unlike-post').addClass('like-post');
    }
}