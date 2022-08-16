import React from 'react';
import { Link, Navigate } from "react-router-dom";
import { connect } from 'react-redux';
import { logIn } from '../../Actions/Common/common';

const mapStateToProps = (state: any) => ({
      isLoggedIn: state.common.isLoggedIn
});
  
const mapDispatchToProps = (dispatch: any) => ({
    onLogIn: () => dispatch(logIn)
});

interface RegisterFormProps{
    onLogIn?: ()=>{},
    isLoggedIn: boolean
}

class RegisterForm extends React.Component<RegisterFormProps> {
    render(): React.ReactNode {
        return (
            <div className='card'>
                <div className='card-body'>
                    <div className='m-1 p-1 display-6'>
                        <label>Rejestracja</label>
                    </div>
                    <div className='m-1 p-1'>
                        <label>Email</label>
                        <input className='form-control'>
                        </input>
                    </div>
                    <div className='m-1 p-1'>
                        <label>Hasło</label>
                        <input className='form-control' type='password'>
                        </input>
                    </div>
                    <div className='m-1 p-1'>
                        <label>Potwierdź hasło</label>
                        <input className='form-control' type='password'>
                        </input>
                    </div>
                    <div className='m-1 p-1 d-flex justify-content-between'>
                        <div className="my-auto d-flex gap-2">
                            <Link to={'/'}>Wróć do logowania</Link>
                        </div>
                        <input className='btn btn-outline-primary' 
                            onClick={()=>this.props.onLogIn!()} 
                            type='submit' value="Zarejestruj"/>
                            {
                                this.props.isLoggedIn &&
                                <Navigate to="/dashboard"/>
                            }
                    </div>
                </div>
            </div>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterForm);
