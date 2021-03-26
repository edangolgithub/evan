import React, { Component } from 'react'
import { Col, Row,  Container } from 'react-bootstrap'
import { Caroselfunction } from "../Functions/Caroselfunction";

export class Home extends Component {
    state = {
        opts: {
            width: '100%',
        }
    }
    render() {
        return (
            <div>
                <Container fluid>
                    <Row>
                        <Col className="slider">
                            <Caroselfunction />
                        </Col>
                    </Row>
                </Container>
                <Container>
                    <Row className="iconrow">
                        <Col>
                            <div>
                                <span class="material-icons">
                                    person_pin_circle
                                </span>
                                <p>
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum 
                                blandit tortor lorem, vel posuere lorem facilisis eget. Suspendisse elit 
                                erat, dapibus nec posuere a, auctor sit amet nisi. Aenean rutrum mollis
                                 tincidunt. Fusce volutpat scelerisque lorem sit amet laoreet. Morbi 
                                 consectetur metus in justo aliquam consequat. Aliquam vitae vulputate

                                </p>
                            </div>
                        </Col>
                        <Col>
                            <div>
                            <span class="material-icons">
                                    tram
                                </span>
                                <p>
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum 
                                blandit tortor lorem, vel posuere lorem facilisis eget. Suspendisse elit 
                                erat, dapibus nec posuere a, auctor sit amet nisi. Aenean rutrum mollis
                                 tincidunt. Fusce volutpat scelerisque lorem sit amet laoreet. Morbi 
                                 consectetur metus in justo aliquam consequat. Aliquam vitae vulputate

                                </p>
                            </div>
                        </Col>
                        <Col>
                            <div>
                            <span class="material-icons">
                                    local_airport
                                </span>
                                <p>
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum 
                                blandit tortor lorem, vel posuere lorem facilisis eget. Suspendisse elit 
                                erat, dapibus nec posuere a, auctor sit amet nisi. Aenean rutrum mollis
                                 tincidunt. Fusce volutpat scelerisque lorem sit amet laoreet. Morbi 
                                 consectetur metus in justo aliquam consequat. Aliquam vitae vulputate

                                </p>
                            </div>
                        </Col>
                    </Row>
                    <Row className="contentrow">
                        <Col md="3" className="side">
                            <p className="card">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum blandit tortor lorem, vel posuere lorem facilisis eget. Suspendisse elit erat, dapibus nec posuere a, auctor sit amet nisi. Aenean rutrum mollis tincidunt. Fusce volutpat scelerisque lorem sit amet laoreet. Morbi consectetur metus in justo aliquam consequat. Aliquam vitae vulputate risus. Ut laoreet, urna ac varius semper, dui ipsum faucibus leo, vel laoreet urna augue at lorem. Fusce eu interdum leo, sit amet malesuada ex. Morbi lectus risus, porttitor vel sapien ac, iaculis consectetur dui. Fusce hendrerit lobortis mauris non lacinia. Donec sed nisl sed massa faucibus tempus. Praesent fringilla nisl vel felis varius, et finibus risus vestibulum. Morbi sodales, augue a tincidunt.

                           </p>
                        </Col>
                        <Col md="9" className="main">
                            <p className="card">
                                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum blandit tortor lorem, vel posuere lorem facilisis eget. Suspendisse elit erat, dapibus nec posuere a, auctor sit amet nisi. Aenean rutrum mollis tincidunt. Fusce volutpat scelerisque lorem sit amet laoreet. Morbi consectetur metus in justo aliquam consequat. Aliquam vitae vulputate risus. Ut laoreet, urna ac varius semper, dui ipsum faucibus leo, vel laoreet urna augue at lorem. Fusce eu interdum leo, sit amet malesuada ex. Morbi lectus risus, porttitor vel sapien ac, iaculis consectetur dui. Fusce hendrerit lobortis mauris non lacinia. Donec sed nisl sed massa faucibus tempus. Praesent fringilla nisl vel felis varius, et finibus risus vestibulum. Morbi sodales, augue a tincidunt pellentesque, urna turpis rhoncus urna, non laoreet nisi felis eu lorem. Mauris et cursus magna, et iaculis elit.

                                In et sem eu eros congue interdum sit amet sit amet urna. Donec sodales ex molestie, porttitor dui non, ultrices nulla. Aenean ut hendrerit tellus. Maecenas vitae sapien nec orci placerat ornare eget id eros. Fusce pretium eu magna ac euismod. Aenean egestas a neque facilisis molestie. In et sem congue turpis pretium dictum. Ut accumsan, arcu nec pellentesque semper, erat est ornare ligula, non ornare velit tellus vel massa. Integer luctus a nibh ut euismod. Nunc laoreet tellus vel massa imperdiet convallis.

                                Vivamus ut molestie ligula. Duis ut lorem a lectus pretium rutrum. Etiam condimentum dignissim nisi, eu rhoncus nisl auctor sit amet. Vestibulum tincidunt hendrerit auctor. Ut quis eros in tellus luctus blandit. Phasellus pretium massa scelerisque, faucibus dolor id, rutrum mauris. Ut pellentesque quis turpis sed cursus. Nulla quis pharetra felis, a sollicitudin mauris.

                                Fusce nibh tortor, tempor ut dictum tempor, placerat non metus. Donec quis molestie lectus, at pharetra eros. Nunc sodales neque enim. Nulla ac fermentum orci. Suspendisse sit amet lectus libero. Phasellus facilisis iaculis eleifend. Integer placerat ante quis odio egestas sodales vel in ante. Proin sollicitudin tristique volutpat. Aenean fringilla nisi sit amet magna accumsan, non tempor metus gravida. Duis dolor massa, auctor a risus non, aliquet aliquam velit.

                                Suspendisse et elementum sapien, vel finibus dui. Integer quis mauris iaculis, vehicula neque ut, ultrices justo. Morbi rhoncus pretium eros, non suscipit lacus finibus in. Ut lacinia iaculis placerat. Fusce vel erat mattis, pellentesque sem id, luctus lacus. Curabitur blandit metus non dui consectetur, ac volutpat metus elementum. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.
                       </p>
                        </Col>
                    </Row>

                </Container>
            </div>
        )
    }
}

export default Home
