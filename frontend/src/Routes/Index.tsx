import React from 'react';
import LoginForm from '../Components/Account/LoginForm';

class Index extends React.Component{
  render(): React.ReactNode {
    return (
      <div>
        <div className="App row m-2 gap-2 border rounded-3">
          <div className='col'>
          </div>
          <div className=' bg-light bg-gradient col-6 p-3'>
            <LoginForm/>
          </div>
          <div className='col'>
          </div>
        </div>
      </div>
    );
  }
}

export default Index;
