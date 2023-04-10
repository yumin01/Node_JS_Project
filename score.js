const express = require('express');
const app = express();

let users = [];                                     //App.js 에서 users 배열 추가 
let usersid = [];                                   //user id 배열 추가 

app.use(express.json());

app.get('/', (req, res)=>{
    res.send('hello world!');
});

app.get('/scores/:id', (req, res)=>{
    console.log('id :' + req.params.id);                    //Unity에서 req에 id를 무었을 보냈는지 확인 
    
    let user = users.find(x=>x.id == req.params.id);        //배열 값에 유저가 있나 확인
   
    if(user === undefined)                                  //if(user == null) => if(user === undefined)    
    { 
        res.send({
            cmd : 1103,
            message : '잘못된 id 입니다. '
        });
    }
    else 
    {
        res.send({
            cmd : 1102,
            message : '',
            result : user                                     //찾은 유저를 Unity에 전송 
        });
    }   
});

app.get('/scores/top3', (req, res)=>{
    let result = users.sort(function (a,b) {                    //Users를 sort
        return b.score - a.score;
    });

    result = result.slice(0,3);                                 // 0 ~ (n - 1)  1~3등을 보여준다. 
    
    res.send({
        cmd : 1101,
        message : '',
        result                                                  //찾은 유저를 Unity에 전송 
    });

});

app.post('/scores', (req, res)=>{
    const {id ,score} = req.body;
    console.log(req.body);
    let result = {
        cmd : -1,
        messasge : ''
    };

    let user = users.find(x=>x.id == id); 

    if(user === undefined)
    {
        users.push({id, score});
        result.cmd = 1001;
        result.message = '점수가 신규 등록 되었습니다.'
    }
    else
    {
        console.log(score, id , user.score);

        if(score > user.score)
        {
            user.score = score;
            result.cmd = 1002;
            result.message = '점수가 갱신 되었습니다. '
        }
        else
        {
            result.cmd = 1003;
        }
    }
    console.log(users);
    res.send(result);

});

// let usersid = []         위쪽에 선언
app.post('/register', (req, res)=>{
   
    console.log(req.body.userid.id);                    //req Unity3D에서 쏜 값을 가져온다. 
    let id = req.body.userid.id;
    let password = req.body.userid. password;
    let result = {
        cmd : -1,
        message : ''
    }
    let userid = usersid.find(x=>x.id == id);

    if(userid === undefined)
    {
        usersid.push({id, password});
        result.cmd = 1101;
        result.message = '신규 아이디가 등록 되었습니다. '
    }
    else
    {
        result.cmd = 1102;
        result.message = '이미 존재하는 아이디 입니다. '      
    }   
    res.send(result);
});



app.listen(3030, ()=> {
    console.log('server is running at 3030 port');
})