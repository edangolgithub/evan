import React, { Component } from 'react'
import * as functions from '../../FakeApi/Services'
export class User extends Component {
    constructor(props) {
        super(props);
        this.GetUsers = this.GetUsers.bind(this)
    }
    state = { Users: [] }
    GetUsers() {
        const users = functions.GetUsers();
        users.then(data => {
            this.setState({ Users: data });
        })
    }
    render() {
        return (
            <div>
                <button onClick={this.GetUsers} className="btn btn-primary m-1" type="button">Get</button>
                <div className="table-responsive">
                    <table className="table table-bordered table-sm">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">User Name</th>
                                <th scope="col">Password</th>
                                <th scope="col">Email</th>
                                <th scope="col">Phone</th>
                                <th scope="col">Tole</th>
                                <th scope="col">Address</th>
                                <th scope="col">City</th>
                                <th scope="col">Date</th>
                                <th scope="col">Role</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.Users &&
                                this.state.Users.map((user, key) =>
                                    <tr key={key}>
                                        <td>{user.userId}</td>
                                        <td>{user.userName}</td>
                                        <td>{user.password}</td>
                                        <td>{user.userEmail}</td>
                                        <td>{user.userPhone}</td>
                                        <td>{user.userTole}</td>
                                        <td>{user.userAddress}</td>
                                        <td>{user.userCity}</td>
                                        <td>{user.userCreatedOn}</td>
                                        <td>{user.userRoleId}</td>
                                    </tr>
                                )

                            }

                        </tbody>
                    </table>
                </div>

            </div>
        )
    }
}

export default User
