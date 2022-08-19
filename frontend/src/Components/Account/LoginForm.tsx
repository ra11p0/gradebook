import React from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';
import AccountRepository from '../../ApiClient/Account/Queries/Repositories/AccountRepository';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: (token: string, refreshToken: string) => dispatch({
        ...logIn, 
        isLoggedIn: true,
        token: token,
        refreshToken: refreshToken
    })
});

interface LogInProps{
    onLogIn?: (token: string, refreshToken: string)=>{},
    isLoggedIn: boolean,
}

interface LogInState{
    username?: string,
    password?: string
}

class LoginForm extends React.Component<LogInProps, LogInState> {
    constructor(props: LogInProps) {
        super(props);
        this.state = {
            username: '',
            password: ''
        }
    }
    onLogIn(){
        AccountRepository.logIn({
            username: this.state.username!,
            password: this.state.password!
        }).then((r: any)=>{
            this.props.onLogIn!( r.data.token, r.data.refreshToken );
        }).catch((r:any)=>{
            alert("error");
        });
    }
    render(): React.ReactNode {
        return (
            <div className='card'>
                {
                    this.props.isLoggedIn &&
                    <Navigate to="/dashboard"/>
                }
                <div className='card-body'>
                    <div className='m-1 p-1 display-6'>
                        <label>Logowanie</label>
                    </div>
                    <div className='m-1 p-1'>
                        <label>Login</label>
                        <input className='form-control' 
                            value={this.state.username} 
                            onChange={(e) => this.setState({
                                ...this.state,
                                username: e.target.value
                            })}>
                        </input>
                    </div>
                    <div className='m-1 p-1'>
                        <label>Hasło</label>
                        <input className='form-control' 
                            type='password'
                            value={this.state.password} 
                            onChange={(e) => this.setState({
                                ...this.state,
                                password: e.target.value
                            })}
                            >
                        </input>
                    </div>
                    <div className='m-1 p-1 d-flex justify-content-between'>
                        <div className="my-auto d-flex gap-2">
                            <Link to={'account/register'}>Załóz konto</Link>
                            <Link to={''}>Zmień hasło</Link>
                            <Link to={''}>Przywróć dostęp</Link>
                        </div>
                        <input className='btn btn-outline-primary' 
                            onClick={()=> this.onLogIn!()} 
                            type='submit' value="Zaloguj"/>
                    </div>
                </div>
            </div>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginForm);
