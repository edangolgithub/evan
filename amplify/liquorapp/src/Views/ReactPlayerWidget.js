import React, { Component } from 'react'
import ReactPlayer from 'react-player'
export class ReactPlayerWidget extends Component {
    render() {
        return (
            <div>
                <ReactPlayer url='https://www.youtube.com/watch?v=ysz5S6PUM-U' />
            </div>
        )
    }
}

export default ReactPlayerWidget
