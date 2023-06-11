import React from "react";

const Joke = ({joke})=>{
    const {setUp, punchLine, usersJokes} = joke
    console.log(joke)
    return(
        <div className="card card-body bg-light mb-3">
        <h5>{setUp}</h5>
        <h5>{punchLine}</h5>
        <span>Likes: {usersJokes.filter(j => j.liked).length}</span>
        <br />
        <span>Dislikes: {usersJokes.filter(j => !j.liked).length}</span>
      </div>
   
    )
}
export default Joke