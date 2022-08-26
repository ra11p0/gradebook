import React from 'react';
import { Link, Navigate, Route, Routes } from "react-router-dom";
import { connect } from 'react-redux';
import ManageStudentsMenu from './ManageStudentsMenu';
import Invitations from './Invitations';
import StudentsList from './StudentsList';


const mapStateToProps = (state: any) => ({
     
});
  
const mapDispatchToProps = (dispatch: any) => ({
    
});

interface ManageStudentsProps{

}

class ManageStudents extends React.Component<ManageStudentsProps> {
    render(): React.ReactNode {
        return (
            <div className='row'>
                <div className='col-3'>
                    <ManageStudentsMenu/>
                </div>
                <div className='col'>
                    <Routes>
                        <Route path='invitations' element={<Invitations/>}/>
                        <Route path='studentsList' element={<StudentsList/>}/>
                    </Routes>
                </div>
            </div>
          );
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(ManageStudents);
