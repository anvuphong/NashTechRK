import axios from "axios";
import React, { useEffect, useState } from "react";
import { Button, Modal, Table } from "react-bootstrap";

const RequestControlComponet = () => {
    const [requestList, setRequestList] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');

    const [modalShow, setModalShow] = useState(false);

    useEffect(() => {
        (async () => {
            let isCancel = false;
            //get category
            await axios({
                method: 'GET',
                url: 'https://localhost:7279/api/book-request'
            }).then(response => {
                if (!isCancel) {
                    setRequestList(response.data);
                    setIsLoading(false);
                }
            }).catch((error) => {
                if (!isCancel) {
                    setIsLoading(false);
                    setError('Something wrong');
                }
            });
            return () => {
                isCancel = true;
            }
        })()
    }, []);

    //Status Request
    if (isLoading) return (
        <div>Loading</div>
    );
    if (error) return (
        <div style={{ color: 'red' }}>{error}</div>
    );

    const userId = 1;

    //Confirm
    // const handleClick=()=>{
    //     setModalShow(true)
    // }

    //Process
    const handleProcess = (evt) => {
        if (evt.target.name === 'reject') {
            (async () => {
                let isCancel = false;
                await axios({
                    method: 'PUT',
                    url: 'https://localhost:7279/api/book-request',
                    data: {
                        requestId: evt.target.id,
                        processByUserId: userId,
                        status: 1
                    }
                })
                return () => {
                    isCancel = true;
                }
            })();
        }
        if (evt.target.name === 'approve') {
            (async () => {
                let isCancel = false;
                await axios({
                    method: 'PUT',
                    url: 'https://localhost:7279/api/book-request',
                    data: {
                        requestId: evt.target.id,
                        processByUserId: userId,
                        status: 2
                    }
                })
                return () => {
                    isCancel = true;
                }
            })();
        }
        window.location.reload();
    }


    return (
        <div>
            {/* <Modal show={modalShow} onHide={setModalShow(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Modal heading</Modal.Title>
                </Modal.Header>
                <Modal.Body>Woohoo, you're reading this text in a modal!</Modal.Body>
            </Modal> */}
            <Table striped bordered hover className="categories-table">
                <thead>
                    <tr>
                        <th>Request</th>
                        <th>RequestDate</th>
                        <th>List Book Requested</th>
                        <th>Process</th>
                    </tr>
                </thead>
                <tbody>
                    {requestList.map(request => (
                        <tr key={request.requestId}>
                            <td></td>
                            <td>{request.dateOfRequest}</td>
                            <td></td>
                            {(request.status === 1 && <td>Rejected</td>)}
                            {(request.status === 2 && <td>Approved</td>)}
                            {(request.status === 0) &&
                                <td>
                                    <Button
                                        name="approve"
                                        variant="outline-success"
                                        id={request.requestId}
                                        onClick={handleProcess}
                                        style={{ marginRight: '10px' }}
                                    >
                                        Approve
                                    </Button>
                                    <Button
                                        name="reject"
                                        variant="outline-danger"
                                        id={request.requestId}
                                        onClick={handleProcess}
                                    >
                                        Reject
                                    </Button>
                                </td>
                            }

                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    )
}
export default RequestControlComponet;