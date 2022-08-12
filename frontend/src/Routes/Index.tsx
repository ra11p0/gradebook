import React from 'react';
import LoginForm from '../Components/Account/LoginForm';

class Index extends React.Component{
  render(): React.ReactNode {
    return (
      <div>
        <div className="App row m-2 gap-2 border rounded-3">
          <div className='col'>
            placeholder
          </div>
          <div className='bg-primary bg-gradient col p-3'>
            <LoginForm/>
          </div>
        </div>
      </div>
      
    );
  }
}

export default Index;
