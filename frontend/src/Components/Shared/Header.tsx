import React from 'react';

class Header extends React.Component {
    render(): React.ReactNode {
        return (
            <header className='p-4 bg-primary bg-gradient'>
                <div className='d-flex'>
                    <label className='text-light'>
                        Gradebook
                    </label>
                </div>
            </header>
          );
    }
}

export default Header;
