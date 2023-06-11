import axios from "axios";
import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import useInterval from "./UseInterval";

const Home = () => {

  const [joke, setJoke] = useState({
    id: '',
    jokeId: '',
    setUp: '',
    punchLine: '',
    likesCount: '',
    dislikesCount: ''
  });
  const [userInteractionStatus, setUserInteractionStatus] = useState('');

  const updateCounts = async () => {
    const { id } = joke;
    if (!id) {
      return;
    }
    const { data } = await axios.get(`/api/jokes/getlikescount/${id}`);
  
    setJoke({ ...joke, likesCount: data.likes, dislikesCount: data.dislikes })

  }
  useInterval(updateCounts, 500);

  useEffect(() => {
    getJoke();
  }, [])

  const getJoke = async () => {
    const { data } = await axios.get('/api/jokes/getjoke');
    const { data: interactionStatus } = await axios.get(`/api/jokes/getinteractionstatus/${data.id}`);
    setJoke(data);
    setUserInteractionStatus(interactionStatus.status);

  }

  const interactWithJoke = async liked => {
    const { id } = joke;
    await axios.post(`/api/jokes/interactwithjoke`, { jokeId: id, liked });
    const { data: interactionStatus } = await axios.get(`/api/jokes/getinteractionstatus/${id}`);
    setUserInteractionStatus(interactionStatus.status);
  }

  const refresh = () => {
    window.location.reload();
  }

  const { setUp, punchLine, likesCount, dislikesCount } = joke
  const canLike = userInteractionStatus !== 'Liked';
  const canDislike = userInteractionStatus !== 'Disliked';

  return (
    <div>

      <div className="container" style={{ marginTop: 60 }}>
        <div className="row" style={{ minHeight: "80vh", display: "flex", alignItems: "center" }} >

          <div className="col-md-6 offset-md-3 bg-light p-4 rounded shadow">
            {setUp && <div>
              <h4>{setUp}</h4>
              <h4>{punchLine}</h4>
              <div>
                {userInteractionStatus !== 'Unauthenticated' && <>
                  <button disabled={!canLike} className="btn btn-success" onClick={() => interactWithJoke(true)}>
                    Like
                  </button>
                  <button disabled={!canDislike} className="btn btn-danger" onClick={() => interactWithJoke(false)}>
                    Dislike
                  </button>
                </>}
                {userInteractionStatus === 'Unauthenticated' &&
                  <div>
                    <Link to="/login">
                      Login to your account to like/dislike this joke
                    </Link>
                  </div>}
                <br />
                <h4>Likes: {likesCount}</h4>
                <h4>Dislikes: {dislikesCount}</h4>
                <h4>
                  <button className="btn btn-link" onClick={refresh}>Refresh</button>
                </h4>
              </div>
            </div>}
            {!setUp && <h3>Loading...</h3>}

          </div>
        </div>
      </div>
    </div>

  )
}
export default Home