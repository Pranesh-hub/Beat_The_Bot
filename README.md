<<<<<<< HEAD
# <u>Beat The Bot </u>
## <u>Week 1:Assignment</u>

## <u>Index</u>
1. [Environment](#initial-environment-description)
2. [Phase 1 - Initial Training](#phase-1--initial-training)
3. [Phase 2 - Improvement](#phase-2--debugging-and-improvement)
4. [Reflection Questions](#reflection-questions)

### Initial Environment Description
The pong environment provided for the agent to train for consists of the
following :
1. Player ( Left & Right) : These are the Game Objects controlled by the agent to move vertically to prevent the ball from hitting their goals.
2. Goal : There are triggers on both sides. When the ball collides with these, it increases the score for the other player. It is the main reward and penalty of the agents.
	1. Move Speed = 8
3. Ball : 
	1. Move Speed = 7
	2. Max Spawn Y Offset = 1
	3. Max Launch Angle = 30
	4. Radius = 5
4. Walls : The boundaries of the game environment. Reflects the ball when hit.
5. Score : Tracks progress of agent on both sides

<img width="625" height="355" alt="image WZOOI3" src="https://github.com/user-attachments/assets/8ffa8c26-6043-4b7e-beeb-684cf85c0eaa" />

<p align="center"> Game Scene </p>

---

## <u>Phase 1 : Initial Training</u>
### Initial Reward Design
The rewards are are based on two factors:
• Goals : Scoring a goal gives A +1.0 reward to winner, while the losing agent receives a -1.0 reward. Overall, this results in a net 0 reward given.
• Time : A small negative reward (-0.0002) is given to discourage longer rallies. This causes a net negative reward.

#### Reward Curves
<img width="751" height="318" alt="image 9G2CI3" src="https://github.com/user-attachments/assets/2f7d316e-3ef0-4491-a056-2322ebc7af1f" />

<p align="center"> Cumulative Reward </p>

---

## <u>Phase 2 : Debugging and Improvement</u>
#### Observation and Changes in Agent Behaviour

| Observation | Inference | Change |
|:----------:|:---------:|:------:|
| Reward curve is decreasing | Net negative reward given | Reward given for paddle hitting the ball: +0.005 |
| Episode length increases very high (20 to 135) | Time penalty is ineffective due to goal reward | Reward given for goal: +1 (Win), 0 (Loss) |
| Policy Loss showing large oscillations | Training is unstable | No change for now |
| Value Loss decreases steadily | Good value estimation | No change |

#### Extra Changes
- Reward penalty for time: -0.00002 (Adjustment)
- Max_steps: 500000 to 300000
- Summary_freq=5000 (More data points)

#### Reward Curves
<img width="782" height="324" alt="image I01QI3" src="https://github.com/user-attachments/assets/356246b1-863c-41e8-b357-214a4ec8cfac" />

<p align="center"> Cumulative Reward </p>

---

## <u>Reflection Questions</u>
1. **What was the first major flaw in your agent’s behaviour?**
- The agent learned to stall the game by keeping the ball instead of trying to score. This resulted in increased episode length.
2. **What was the primary cause of this flaw?**
- The major cause was the flawed reward system. Both players were controlled by the same agent. Scoring a goal gave rewards +1 and -1 got cancelled out. So, scoring was not awarded properly.
3. **What specific change did you make to address the issue?**
- The major change was removing the losing penalty ( -1 -> 0 ). Another change was adding a reward for hitting a paddle ( 0 -> 0.005)
4. **How did the reward curve change after the fix?**
- The reward curve changed from negative and decreasing to positive and stable. The agent reached an equilibrium and became more stable than before.
- The learning speed improved as compared to before. The policy curve is stable and decreasing now, indicating better learning.
- However, the final performance doesn’t improve much.
5. **What does this tell you about training instability in reinforcement learning?**
- In reinforcement learning, the major goal of the agent is to maximise rewards. So, this might lead to the agent exploiting rewards instead of actually doing its purpose. The trainer needs to give priority to the major goal while also ensuring that the reward system is balanced. This leads to instable learning and lot of trial and error.
=======
"# Beat_The_Bot" 
>>>>>>> upstream/main
