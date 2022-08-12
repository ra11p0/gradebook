import React from 'react';
import { Outlet, Link } from "react-router-dom";

class LoginForm extends React.Component {
    render(): React.ReactNode {
        return (
            <div className='card'>
                <div className='card-body'>
                    <div className='m-1 p-1'>
                        <label>Login</label>
                        <input className='form-control'>
                        </input>
                    </div>
                    <div className='m-1 p-1'>
                        <label>Password</label>
                        <input className='form-control' type='password'>
                        </input>
                    </div>
                    <div className='m-1 p-1 text-end'>
                        <input className='btn btn-outline-primary' type='submit'/>
                        <Link to="/dashboard">Dashboard</Link> |{" "}
                        <Link to="/expenses">Expenses</Link>
                    </div>
                </div>
            </div>
          );
    }
}

export default LoginForm;
