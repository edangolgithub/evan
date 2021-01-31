import YouTube from 'react-youtube';
import React, { Component } from 'react'

export class YoutubeWidget extends Component {
    _onReady(event) {
        // access to player in all event handlers via event.target
        event.target.pauseVideo();
      }
      constructor() {
        super();
        this._onReady = this._onReady.bind(this);
      }
      state = {
        opts: {
        //  height: '390',
          width: '100%',
          playerVars: {
            // https://developers.google.com/youtube/player_parameters
            autoplay: 0
          }
        }
      }
    render() {
        return (
            <div>
                   <YouTube videoId="2g811Eo7K8U" opts={this.state.opts} onReady={this.state._onReady} />;
            </div>
        )
    }
}

export default YoutubeWidget
