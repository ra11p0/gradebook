import React from 'react';
import { connect } from 'react-redux';
import { Navigate } from 'react-router-dom';
import { logOut } from '../../Actions/Account/accountActions';
import AccountRepository from '../../ApiClient/Account/Queries/Repositories/AccountRepository';

const mapStateToProps = (state: any) => {
    return {
      isLoggedIn: state.common.isLoggedIn,
      username: state.common.session?.username
    }
};

const mapDispatchToProps = (dispatch: any) => ({
    logOutHandler: () => dispatch(logOut)
});
  
interface ProfileProps{
    isLoggedIn?: boolean;
    logOutHandler?: ()=>void;
    username: string
}

interface ProfileState{
    isLoggedIn?: boolean,
    weather?: any
}

class Profile extends React.Component<ProfileProps, ProfileState>{
    constructor(props: ProfileProps) {
        super(props);
        this.state = {
            isLoggedIn: props.isLoggedIn,
        };
    }
    async componentDidMount(){
        AccountRepository.getWeather()
            .then((e: any)=>{
                this.setState({
                    ...this.state,
                    weather: JSON.stringify(e.data)
                });
            });
    }
    render(): React.ReactNode {
        return (
            <div>
                profile
                and weather: 
                {this.state.weather}
                {
                    !this.props.isLoggedIn &&
                    <Navigate to='/'/>
                }
            </div>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Profile);
