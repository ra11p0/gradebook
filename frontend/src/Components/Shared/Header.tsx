import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { logOut } from '../../Actions/Common/common';

const mapStateToProps = (state: any) => {
    return {
      isLoggedIn: state.common.isLoggedIn
    }
};

const mapDispatchToProps = (dispatch: any) => ({
    logOutHandler: () => dispatch(logOut)
});
  
interface HeaderProps{
    isLoggedIn?: boolean;
    logOutHandler?: ()=>void;
}

interface HeaderState{
    isLoggedIn?: boolean
}

class Header extends React.Component<HeaderProps, HeaderState>{
    constructor(props: HeaderProps) {
        super(props);
        this.state = {
            isLoggedIn: props.isLoggedIn
        };
    }
    logOut(): void{
        this.props.logOutHandler!();
    }
    render(): React.ReactNode {
        return (
            <header className='p-4 bg-light bg-gradient'>
                <div className='d-flex justify-content-between'>
                    <Link to="/" className='text-dark display-6 text-decoration-none'>
                        Gradebook
                    </Link>
                    <div className='my-auto'>
                    {
                        this.props.isLoggedIn &&
                        <div className='d-flex gap-2'>
                            <Link to='/dashboard' className='btn btn-outline-primary' >Dashboard</Link>
                            <a className='btn btn-outline-primary' onClick={() => this.logOut()}> Log out</a>
                        </div>
                    }
                    </div>
                </div>
            </header>
        );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(Header);
