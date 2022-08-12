import React from 'react';
import LoginForm from '../Components/Account/LoginForm';

class App extends React.Component{
  render(): React.ReactNode {
    return (
      <div className="App row m-2 gap-2 border rounded">
        <div className='col'>
          placeholder
        </div>
        <div className='bg-primary bg-gradient col p-3'>
          <LoginForm/>
        </div>
      </div>
    );
  }
}

export default App;
