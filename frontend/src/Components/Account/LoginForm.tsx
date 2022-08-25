import React from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Account/accountActions';
import AccountRepository from '../../ApiClient/Account/AccountProxy';
import { withTranslation } from 'react-i18next';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: (token: string,
        refreshToken: string,
        username: string,
        userId: string,
        roles: string[]) => dispatch({
        ...logIn, 
        isLoggedIn: true,
        token: token,
        refreshToken: refreshToken,
        username: username,
        userId: userId,
        roles: roles
    })
});

interface LogInProps{
    onLogIn?: (token: string,
        refreshToken: string,
        username: string,
        userId: string,
        roles: string[])=>{},
    isLoggedIn: boolean,
    t: any
}

interface LogInState{
    username?: string,
    password?: string,
    loginFailed?: boolean
}

class LoginForm extends React.Component<LogInProps, LogInState> {
    constructor(props: LogInProps) {
        super(props);
        this.state = {
            username: '',
            password: '',
            loginFailed: false
        }
    }
    onLogIn(){
        AccountRepository.logIn({
            username: this.state.username!,
            password: this.state.password!
        }).then((r: any)=>{
            this.props.onLogIn!( r.data.access_token, r.data.refreshToken, r.data.username, r.data.userId, r.data.roles );
        }).catch((r:any)=>{
            this.setState({
                ...this.state,
                loginFailed: true
            });
        });
    }
    render(): React.ReactNode {
        const { t } = this.props;
        return (
            <div className='card m-3 p-3'>
        
                <div className='card-body'>
                    <div className='m-1 p-1 display-6'>
                        <label>{t('loging')}</label>
                    </div>
                    {
                        this.state.loginFailed &&
                        <div className='m-1 p-1 alert alert-danger'>
                            Logowanie nieudane
                        </div>
                    }
                    <div className='m-1 p-1'>
                        <label>{t('email')}</label>
                        <input className='form-control' 
                            value={this.state.username} 
                            onChange={(e) => this.setState({
                                ...this.state,
                                username: e.target.value
                            })}>
                        </input>
                    </div>
                    <div className='m-1 p-1'>
                        <label>{t('password')}</label>
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
                            <Link to={'register'}>{t('register')}</Link>
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

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(LoginForm));
