import { faLanguage } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React from 'react';
import { Dropdown } from 'react-bootstrap';
import { withTranslation } from 'react-i18next';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { logOut } from '../../Actions/Account/accountActions';

const mapStateToProps = (state: any) => {
    return {
      isLoggedIn: state.common.isLoggedIn,
      username: state.common.session?.username
    }
};

const mapDispatchToProps = (dispatch: any) => ({
    logOutHandler: () => dispatch(logOut)
});
  
interface HeaderProps{
    isLoggedIn?: boolean;
    logOutHandler?: ()=>void;
    username: string,
    i18n: any;
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
        const { i18n } = this.props;
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
                            <Link to='/account/profile' className='btn btn-link' >{this.props.username}</Link>
                            <Link to='/dashboard' className='btn btn-outline-primary' >Dashboard</Link>
                            <a className='btn btn-outline-primary' onClick={() => this.logOut()}> Log out</a>
                        </div>
                    }
                    <Dropdown>
                        <Dropdown.Toggle>
                            <FontAwesomeIcon icon={faLanguage}/>
                        </Dropdown.Toggle>
                        <Dropdown.Menu>
                            <Dropdown.Item onClick={()=>i18n.changeLanguage('pl')}>Polish</Dropdown.Item>
                            <Dropdown.Item onClick={()=>i18n.changeLanguage('en')}>English</Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>
                    
                    </div>
                </div>
            </header>
        );
    }
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(Header));
